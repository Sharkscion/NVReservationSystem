namespace FlightReservation.UI.Common
{
    internal class PassengerEventArgs : EventArgs
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; }
    }
}
