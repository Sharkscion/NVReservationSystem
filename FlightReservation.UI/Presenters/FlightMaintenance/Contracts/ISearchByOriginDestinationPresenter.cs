using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByOriginDestinationPresenter
    {
        public void OnArrivalStationChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnDepartureStationChanged(IFormView source, ChangeEventArgs<string> args);

        public void OnSubmitted(IFormView source, SubmitEventArgs<Tuple<string, string>> args);
    }
}
