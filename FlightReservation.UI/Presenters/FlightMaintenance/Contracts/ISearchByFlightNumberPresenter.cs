namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByFlightNumberPresenter
    {
        #region Functions
        void OnFlightNumberChanged(object? source, EventArgs e);
        void OnSubmitted(object? source, EventArgs e);
        #endregion
    }
}
