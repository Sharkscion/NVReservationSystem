using FlightReservation.Data.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface IAddFlightPresenter
    {
        public void OnAirlineCodeChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnFlightNumberChanged(IFormView source, ChangeEventArgs<int> args);
        public void OnArrivalStationChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnDepartureStationChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnArrivalScheduledTimeChanged(IFormView source, ChangeEventArgs<TimeOnly> args);
        public void OnDepartureScheduledTimeChanged(
            IFormView source,
            ChangeEventArgs<TimeOnly> args
        );
        public void OnSubmitted(IFormView source, FlightEventArgs args);
    }
}
