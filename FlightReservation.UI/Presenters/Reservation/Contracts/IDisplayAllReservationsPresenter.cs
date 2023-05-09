namespace FlightReservation.UI.Presenters.Reservation.Contracts
{
    internal interface IDisplayAllReservationsPresenter
    {
        #region Functions
        public void OnSubmitted(object? source, EventArgs e);

        #endregion
    }
}
