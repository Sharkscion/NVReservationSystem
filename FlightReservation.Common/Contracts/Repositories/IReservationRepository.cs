using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.Common.Contracts.Repositories
{
    /// <summary>
    /// Contract for managing data accesses to reservation entities.
    /// </summary>
    public interface IReservationRepository : IRepository<IReservation> { }
}
