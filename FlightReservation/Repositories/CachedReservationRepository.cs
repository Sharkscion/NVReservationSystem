using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Repositories;

namespace FlightReservation.Repositories
{
    public class CachedReservationRepository : IReservationRepository
    {
        #region Declarations
        private readonly HashSet<IReservation> _reservations;
        #endregion

        #region Constructors
        public CachedReservationRepository()
        {
            _reservations = new HashSet<IReservation>();
        }
        #endregion

        #region Implementations of IReservationRepository
        /// <summary>
        /// Saves reservation in memory.
        /// </summary>
        public bool Save(IReservation item)
        {
            _reservations.Add(item);
            return true;
        }

        /// <summary>
        /// Returns all reservations managed in memory.
        /// </summary>
        public IQueryable<IReservation> GetAll()
        {
            return _reservations.AsQueryable();
        }
        #endregion
    }
}
