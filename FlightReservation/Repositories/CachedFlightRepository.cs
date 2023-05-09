using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Repositories;

namespace FlightReservation.Repositories
{
    public class CachedFlightRepository : IFlightRepository
    {
        #region Declarations
        private readonly HashSet<IFlight> _flights;
        #endregion

        #region Constructors
        public CachedFlightRepository()
        {
            _flights = new HashSet<IFlight>();
        }
        #endregion

        #region Implementations of IFlightRepository
        /// <summary>
        /// Returns all flights managed in memory.
        /// </summary>
        public IQueryable<IFlight> GetAll()
        {
            return _flights.AsQueryable();
        }

        /// <summary>
        /// Saves the new item in memory.
        /// </summary>
        public bool Save(IFlight item)
        {
            _flights.Add(item);
            return true;
        }
        #endregion
    }
}
