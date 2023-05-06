using FlightReservation.Data.Contracts;
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

        public bool Create(IReservation item)
        {
            _reservations.Add(item);
            return true;
        }

        public IQueryable<IReservation> List()
        {
            return _reservations.AsQueryable();
        }

        /*  public IReservation? Find(string PNR)
          {
              return _reservations.Where((item) => item.PNR == PNR).FirstOrDefault();
          }

          public void Save(IReservation reservation)
          {
              _reservations.Add(reservation);
          }

          public IEnumerable<IReservation> ViewAll()
          {
              return _reservations;
          }*/
    }
}
