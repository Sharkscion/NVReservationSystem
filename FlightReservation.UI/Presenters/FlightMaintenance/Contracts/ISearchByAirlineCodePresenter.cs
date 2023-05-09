namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByAirlineCodePresenter
    {
        #region Functions

        void OnAirlineCodeChanged(object? source, EventArgs e);
        void OnSubmitted(object? source, EventArgs e);
        #endregion
    }
}
