using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Repositories;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Comparers;
using FlightReservation.Models.Flight;

namespace FlightReservation.Services
{
    public class FlightService : IFlightService
    {
        #region Declarations
        private readonly IFlightRepository _flightRepository;
        #endregion

        #region Constructors
        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        #endregion

        #region Implementations of IFlightService
        /// <summary>
        /// Creates a new flight.
        /// </summary>
        /// <param name="flight">Item to be saved.</param>
        /// <returns>True if successful, otherwise false.</returns>
        /// <exception cref="DuplicateFlightException">
        /// Thrown when a flight with the same airline code, flight number, departure station,
        /// and arrival station already exists.
        /// </exception>
        public bool Create(IFlight flight)
        {
            if (DoesExists(flight))
            {
                throw new DuplicateFlightException($"Duplicate Flight: {flight}");
            }

            return _flightRepository.Save(flight);
        }

        /// <summary>
        /// Checks if the flight with the same airline code, flight number,
        /// departure station, and arrival station already exists.
        /// </summary>
        public bool DoesExists(IFlight flight)
        {
            var equalityComparer = new FlightEqualityComparer();
            return _flightRepository.GetAll().Contains(flight, equalityComparer);
        }

        /// <summary>
        /// Returns all flights having the same flight number.
        /// </summary>
        public IEnumerable<IFlight> FindAllHaving(int flightNumber)
        {
            return _flightRepository
                .GetAll()
                .Where((flight) => flight.FlightNumber == flightNumber);
        }

        /// <summary>
        /// Returns all flights having the same airline code.
        /// </summary>
        public IEnumerable<IFlight> FindAllHaving(string airlineCode)
        {
            return _flightRepository.GetAll().Where((flight) => flight.AirlineCode == airlineCode);
        }

        /// <summary>
        /// Returns all flights having the same flight designator.
        /// </summary>
        public IEnumerable<IFlight> FindAllHaving(string airlineCode, int flightNumber)
        {
            return _flightRepository
                .GetAll()
                .Where(
                    (flight) =>
                        flight.AirlineCode == airlineCode && flight.FlightNumber == flightNumber
                );
        }

        /// <summary>
        /// Returns all flights having the same origin and destination.
        /// </summary>
        public IEnumerable<IFlight> FindAllHaving(string origin, string destination)
        {
            return _flightRepository
                .GetAll()
                .Where(
                    (flight) =>
                        flight.DepartureStation == origin && flight.ArrivalStation == destination
                );
        }

        /// <summary>
        /// Returns all flights available on the specified flight date.
        ///
        /// If flight date is the current date, return flights with scheduled departured time
        /// at least 1 hour after the current time.
        /// </summary>
        /// <param name="flightDate">Date of the flight.</param>
        /// <param name="airlineCode">Airline code of the flight.</param>
        /// <param name="flightNumber">Flight number of the flight.</param>
        /// <param name="dateTimeProvider">Custom provider of the current date and time.</param>
        public IEnumerable<IFlight> FindAvailableFlightsOn(
            DateTime flightDate,
            string airlineCode,
            int flightNumber,
            IDateTimeProvider? dateTimeProvider = null
        )
        {
            DateTime dateNow = dateTimeProvider?.GetNow() ?? DateTime.Now;

            var availableFlights = FindAllHaving(airlineCode, flightNumber);

            if (flightDate.Date == dateNow.Date)
            {
                return availableFlights.Where(
                    (flight) =>
                        flight.DepartureScheduledTime.Hour > dateNow.Hour
                        && flight.DepartureScheduledTime.Minute >= dateNow.Minute
                );
            }

            return availableFlights;
        }
        #endregion
    }
}
