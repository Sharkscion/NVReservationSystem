

using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.Common.Contracts.Services
{
    public interface IFlightService
    {
        bool Create(IFlight flight);
        IEnumerable<IFlight> FindAllHaving(int flightNumber);
        IEnumerable<IFlight> FindAllHaving(string airlineCode);
        IEnumerable<IFlight> FindAllHaving(string airlineCode, int flightNumber);
        IEnumerable<IFlight> FindAllHaving(string origin, string destination);
        IEnumerable<IFlight> FindAvailableFlightsOn(
            DateTime flightDate,
            string airlineCode,
            int flightNumber,
            IDateTimeProvider? dateTimeProvider = null
        );
        bool DoesExists(IFlight flight);
    }
}
