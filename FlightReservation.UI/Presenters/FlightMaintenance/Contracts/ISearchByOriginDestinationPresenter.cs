namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByOriginDestinationPresenter
    {
        void OnArrivalStationChanged(object? source, EventArgs e);
        void OnDepartureStationChanged(object? source, EventArgs e);
        void OnSubmitted(object? source, EventArgs e);
    }
}
