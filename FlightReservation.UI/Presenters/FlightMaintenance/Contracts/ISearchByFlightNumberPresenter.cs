using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByFlightNumberPresenter
    {
        public void OnFlightNumberChanged(IFormView source, ChangeEventArgs<int> args);

        public void OnSubmitted(IFormView source, SubmitEventArgs<int> args);
    }
}
