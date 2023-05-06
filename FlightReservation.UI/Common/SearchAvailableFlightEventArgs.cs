namespace FlightReservation.UI.Common
{
    internal class SearchAvailableFlightEventArgs : EventArgs
    {
        public DateTime FlightDate { get; set; }
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
    }
}
