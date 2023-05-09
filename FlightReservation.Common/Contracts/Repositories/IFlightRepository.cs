using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.Common.Contracts.Repositories
{
    /// <summary>
    /// Contract for managing data accesses to flight entities.
    /// </summary>
    public interface IFlightRepository : IRepository<IFlight> { }
}
