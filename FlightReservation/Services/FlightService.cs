using FlightReservation.Comparers;
using FlightReservation.Models.Contracts;
using FlightReservation.Models.Flight;
using FlightReservation.Repositories.Contracts;
using FlightReservation.Services.Contracts;

namespace FlightReservation.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public bool Create(IFlight flight)
        {
            if (DoesExists(flight))
            {
                throw new DuplicateFlightException($"Duplicate Flight: {flight}");
            }

            return _flightRepository.Save(flight);
        }

        public bool DoesExists(IFlight flight)
        {
            var equalityComparer = new FlightEqualityComparer();
            return _flightRepository.List().Contains(flight, equalityComparer);
        }

        public IEnumerable<IFlight> FindAllHaving(int flightNumber)
        {
            return _flightRepository.List().Where((flight) => flight.FlightNumber == flightNumber);
        }

        public IEnumerable<IFlight> FindAllHaving(string airlineCode)
        {
            return _flightRepository.List().Where((flight) => flight.AirlineCode == airlineCode);
        }

        public IEnumerable<IFlight> FindAllHaving(string airlineCode, int flightNumber)
        {
            return _flightRepository
                .List()
                .Where(
                    (flight) =>
                        flight.AirlineCode == airlineCode && flight.FlightNumber == flightNumber
                );
        }

        public IEnumerable<IFlight> FindAllHaving(string origin, string destination)
        {
            return _flightRepository
                .List()
                .Where(
                    (flight) =>
                        flight.DepartureStation == origin && flight.ArrivalStation == destination
                );
        }

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
    }
}
