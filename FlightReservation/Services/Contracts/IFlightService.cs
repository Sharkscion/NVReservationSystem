using FlightReservation.Models.Contracts;

namespace FlightReservation.Services.Contracts
{
    public interface IFlightService
    {
        void Create(IFlight flight);
        IEnumerable<IFlight> FindAllHaving(int flightNumber);
        IEnumerable<IFlight> FindAllHaving(string airlineCode);
        IEnumerable<IFlight> FindAllHaving(string airlineCode, int flightNumber);
        IEnumerable<IFlight> FindAllHaving(string origin, string destination);
        IEnumerable<IFlight> FindAvailableFlightsOn(
            DateTime flightDate,
            string airlineCode,
            int flightNumber
        );
        bool DoesExists(IFlight flight);
    }
}
