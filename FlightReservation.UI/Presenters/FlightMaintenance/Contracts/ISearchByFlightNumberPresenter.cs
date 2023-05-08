namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByFlightNumberPresenter
    {
        void OnFlightNumberChanged(object? source, EventArgs e);
        void OnSubmitted(object? source, EventArgs e);
    }
}
