namespace FlightReservation.Common.Contracts.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> List();
        bool Save(T item);
    }
}
