namespace FlightReservation.Models.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();
    }
}
