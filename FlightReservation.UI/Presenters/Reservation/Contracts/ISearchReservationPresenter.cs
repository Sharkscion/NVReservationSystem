namespace FlightReservation.UI.Presenters.Reservation.Contracts
{
    internal interface ISearchReservationPresenter
    {
        #region Functions
        void OnPNRChanged(object? source, EventArgs e);
        void OnSubmitted(object? source, EventArgs e);
        #endregion
    }
}
