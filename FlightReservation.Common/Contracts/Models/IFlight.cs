namespace FlightReservation.Common.Contracts.Models
{
    public interface IFlight
    {
        #region Properties
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureStation { get; set; }
        public TimeOnly ArrivalScheduledTime { get; set; }
        public TimeOnly DepartureScheduledTime { get; set; }
        #endregion

        #region Functions
        IFlight CreateFrom(
            string airlineCode,
            int flightNumber,
            string departureStation,
            string arrivalStation,
            TimeOnly departureScheduledTime,
            TimeOnly arrivalScheduledTime
        );
        #endregion
    }
}
