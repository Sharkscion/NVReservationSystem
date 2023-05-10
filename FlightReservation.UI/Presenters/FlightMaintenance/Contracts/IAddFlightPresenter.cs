namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface IAddFlightPresenter
    {
        #region Functions
        void OnAirlineCodeChanged(object? source, EventArgs e);
        void OnFlightNumberChanged(object? source, EventArgs e);
        void OnArrivalStationChanged(object? source, EventArgs e);
        void OnDepartureStationChanged(object? source, EventArgs e);
        void OnSubmitted(object? source, EventArgs e);
        #endregion
    }
}
