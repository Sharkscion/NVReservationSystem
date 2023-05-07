using FlightReservation.Models.Contracts;
using FlightReservation.Models.Reservation;
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
            if (!_flightService.DoesExists(reservation.FlightInfo))
            {
                throw new FlightDoesNotExistException("Flight does not exists.");
            }

            string PNR = generatePNR();

            IReservation confirmedReservation = reservation.FromBookingReference(PNR);
            _reservationRepository.Save(confirmedReservation);

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
