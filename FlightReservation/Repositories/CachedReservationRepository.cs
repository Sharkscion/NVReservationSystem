using FlightReservation.Models.Contracts;
using FlightReservation.Repositories.Contracts;

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
