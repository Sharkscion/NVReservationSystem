namespace FlightReservation.Utilities
{
    public interface IDateTimeProvider
    {
        public DateTime DateNow { get; set; }
    }
}
