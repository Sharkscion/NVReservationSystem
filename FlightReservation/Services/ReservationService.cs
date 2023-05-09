using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Repositories;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Models.Reservation;
using FlightReservation.Utilities;

namespace FlightReservation.Services
{
    public class ReservationService : IReservationService
    {
        #region Declarations
        private readonly IReservationRepository _reservationRepository;
        private readonly IFlightService _flightService;
        #endregion

        #region Constructors
        public ReservationService(
            IReservationRepository reservationRepository,
            IFlightService flightService
        )
        {
            _reservationRepository = reservationRepository;
            _flightService = flightService;
        }
        #endregion

        #region Implmentations of IReservationService
        /// <summary>
        /// Creates a reservation and save it in a database.
        /// </summary>
        /// <param name="reservation">Item to be saved.</param>
        /// <returns>A booking reference of 6 uppercased-alphanumeric characters.</returns>
        /// <exception cref="FlightDoesNotExistException">Thrown when the booked flight does not exists.</exception>
        public string Create(IReservation reservation)
        {
            if (!_flightService.DoesExists(reservation.FlightInfo))
            {
                throw new FlightDoesNotExistException("Flight does not exists.");
            }

            string PNR = generatePNR();

            IReservation confirmedReservation = reservation.CreateWith(PNR);
            _reservationRepository.Save(confirmedReservation);

            return confirmedReservation.PNR;
        }

        /// <summary>
        /// Finds a reservation with a matching booking reference.
        /// </summary>
        /// <param name="PNR">Booking reference of the reservation.</param>
        /// <returns>The reservation if exists, null otherwise.</returns>
        public IReservation? Find(string PNR)
        {
            return _reservationRepository
                .GetAll()
                .Where((item) => item.PNR == PNR)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns all available reservations.
        /// </summary>
        public IEnumerable<IReservation> ViewAll()
        {
            return _reservationRepository.GetAll();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Generates a unique booking reference.
        /// </summary>
        /// <returns>A 6 uppercased-alphanumeric characters.</returns>
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
        #endregion
    }
}
