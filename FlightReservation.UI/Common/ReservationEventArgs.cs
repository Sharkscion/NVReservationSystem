namespace FlightReservation.UI.Common
{
    internal class ReservationEventArgs : EventArgs
    {
        public FlightEventArgs FlightInfo { get; set; }
        public DateTime FlightDate { get; set; }
        public IEnumerable<PassengerEventArgs> Passengers { get; set; }
    }
}
