namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByAirlineCodePresenter
    {
        void OnAirlineCodeChanged(object? source, EventArgs e);
        void OnSubmitted(object? source, EventArgs e);
    }
}
