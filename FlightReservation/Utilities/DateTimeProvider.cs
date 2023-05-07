namespace FlightReservation.Utilities
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime DateNow { get; set; }

        public DateTimeProvider()
        {
            DateNow = DateTime.Now;
        }
    }
}
