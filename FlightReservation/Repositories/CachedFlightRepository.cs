using FlightReservation.Data.Contracts;
using FlightReservation.Repositories.Contracts;

namespace FlightReservation.Repositories
{
    public class CachedFlightRepository : IFlightRepository
    {
        private readonly HashSet<IFlight> _flights;

        public CachedFlightRepository()
        {
            _flights = new HashSet<IFlight>();
        }

        /*
                public void Save(IFlight flight)
                {
                    _flights.Add(flight);
                }
        
                public IEnumerable<IFlight> FindAllHaving(int flightNumber)
                {
                    return _flights.Where((flight) => flight.FlightNumber == flightNumber);
                }
        
                public IEnumerable<IFlight> FindAllHaving(string airlineCode)
                {
                    return _flights.Where((flight) => flight.AirlineCode == airlineCode);
                }
        
                public IEnumerable<IFlight> FindAllHaving(string airlineCode, int flightNumber)
                {
                    return _flights.Where(
                        (flight) => flight.AirlineCode == airlineCode && flight.FlightNumber == flightNumber
                    );
                }
        
                public IEnumerable<IFlight> FindAllHaving(string origin, string destination)
                {
                    return _flights
                        .Where((flight) => flight.DepartureStation == origin)
                        .Where((flight) => flight.ArrivalStation == destination);
                }
        
                public IFlight? Find(
                    int flightNumber,
                    string airlineCode,
                    string origin,
                    string destination
                )
                {
                    return _flights
                        .Where((flight) => flight.FlightNumber == flightNumber)
                        .Where((flight) => flight.AirlineCode == airlineCode)
                        .Where((flight) => flight.DepartureStation == origin)
                        .Where((flight) => flight.ArrivalStation == destination)
                        .FirstOrDefault();
                }
        */
        public IQueryable<IFlight> List()
        {
            return _flights.AsQueryable();
        }

        public bool Create(IFlight item)
        {
            _flights.Add(item);
            return true;
        }
    }
}
