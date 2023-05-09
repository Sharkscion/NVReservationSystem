namespace FlightReservation.Common.Contracts.Repositories
{
    /// <summary>
    /// Base contract for managing data accesses to business entities.
    /// </summary>
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        bool Save(T item);
    }
}
