using FlightReservation.Data.Contracts;

namespace FlightReservation.Repositories.Contracts
{
    public interface IFlightRepository : IRepository<IFlight>
    {
        /*        void Save(IFlight flight);
                IEnumerable<IFlight> FindAllHaving(int flightNumber);
                IEnumerable<IFlight> FindAllHaving(string airlineCode);
                IEnumerable<IFlight> FindAllHaving(string airlineCode, int flightNumber);
                IEnumerable<IFlight> FindAllHaving(string origin, string destination);
                IFlight? Find(int flightNumber, string airlineCode, string origin, string destination);*/
    }
}
