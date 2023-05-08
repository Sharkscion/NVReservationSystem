namespace FlightReservation.Common.Contracts.Models
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();
    }
}
