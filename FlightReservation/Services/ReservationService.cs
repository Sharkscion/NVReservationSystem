using FlightReservation.Data.Contracts;
using FlightReservation.Data.Reservation;
using FlightReservation.Repositories.Contracts;
using FlightReservation.Services.Contracts;
using FlightReservation.Utilities;

namespace FlightReservation.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IFlightService _flightService;

        public ReservationService(
            IReservationRepository reservationRepository,
            IFlightService flightService
        )
        {
            _reservationRepository = reservationRepository;
            _flightService = flightService;
        }

        public string Create(IReservation reservation)
        {
            IFlight flightInfo = reservation.FlightInfo;

            IFlight? existingFlight = _flightService.Find(
                flightNumber: flightInfo.FlightNumber,
                airlineCode: flightInfo.AirlineCode,
                origin: flightInfo.DepartureStation,
                destination: flightInfo.ArrivalStation
            );

            if (existingFlight is null)
            {
                throw new FlightDoesNotExistException("Flight does not exists.");
            }

            string PNR = generatePNR();

            IReservation confirmedReservation = reservation.FromBookingReference(PNR);
            _reservationRepository.Create(confirmedReservation);

            return confirmedReservation.PNR;
        }

        public IReservation? Find(string PNR)
        {
            return _reservationRepository.List().Where((item) => item.PNR == PNR).FirstOrDefault();
        }

        public IEnumerable<IReservation> ViewAll()
        {
            return _reservationRepository.List();
        }

        private string generatePNR()
        {
            while (true)
            {
                string PNR = PNRGenerator.Generate(length: 6);
                if (Find(PNR) is null)
                {
                    return PNR;
                }
            }
        }
    }
}
