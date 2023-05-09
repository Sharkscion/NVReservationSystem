namespace FlightReservation.Common.Contracts.Models
{
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Returns the current date and time.
        /// </summary>
        DateTime GetNow();
    }
}
