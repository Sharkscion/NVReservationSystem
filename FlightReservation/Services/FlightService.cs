using FlightReservation.Data.Contracts;
using FlightReservation.Data.Flight;
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

        public void Create(IFlight flight)
        {
            IFlight? existingFlight = Find(
                airlineCode: flight.AirlineCode,
                flightNumber: flight.FlightNumber,
                origin: flight.DepartureStation,
                destination: flight.ArrivalStation
            );

            if (existingFlight is not null)
            {
                throw new DuplicateFlightException($"Duplicate Flight: {existingFlight}");
            }

            _flightRepository.Create(flight);
        }

        public IFlight? Find(
            int flightNumber,
            string airlineCode,
            string origin,
            string destination
        )
        {
            return _flightRepository
                .List()
                .Where(
                    (flight) =>
                        flight.FlightNumber == flightNumber
                        && flight.AirlineCode == airlineCode
                        && flight.DepartureStation == origin
                        && flight.ArrivalStation == destination
                )
                .FirstOrDefault();
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
            int flightNumber
        )
        {
            var availableFlights = FindAllHaving(airlineCode, flightNumber);

            if (flightDate.Date == DateTime.Now.Date)
            {
                availableFlights.Where(
                    (flight) =>
                        flight.DepartureScheduledTime.Hour > DateTime.Now.Hour
                        && flight.DepartureScheduledTime.Minute >= DateTime.Now.Minute
                );
            }

            return availableFlights;
        }
    }
}
