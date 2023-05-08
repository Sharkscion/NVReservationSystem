using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Repositories;

namespace FlightReservation.Repositories
{
    public class CachedReservationRepository : IReservationRepository
    {
        private readonly HashSet<IReservation> _reservations;

        public CachedReservationRepository()
        {
            _reservations = new HashSet<IReservation>();
        }

        public bool Save(IReservation item)
        {
            _reservations.Add(item);
            return true;
        }

        public IQueryable<IReservation> List()
        {
            return _reservations.AsQueryable();
        }
    }
}
