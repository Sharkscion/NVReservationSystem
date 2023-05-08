namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByAirlineCodePresenter
    {
        public void OnAirlineCodeChanged(object? source, EventArgs e);
        public void OnSubmitted(object? source, EventArgs e);
    }
}
