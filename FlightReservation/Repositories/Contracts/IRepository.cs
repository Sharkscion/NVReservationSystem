namespace FlightReservation.Repositories.Contracts
{
    public interface IRepository<T>
    {
        IQueryable<T> List();
        bool Create(T item);
    }
}
