namespace FlightReservation.UI.Common
{
    internal class PassengerEventArgs : EventArgs
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        #endregion
    }
}
