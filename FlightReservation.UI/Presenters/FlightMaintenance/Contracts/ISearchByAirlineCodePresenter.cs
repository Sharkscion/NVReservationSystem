using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance.Contracts
{
    internal interface ISearchByAirlineCodePresenter
    {
        public void OnAirlineCodeChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnSubmitted(IFormView source, SubmitEventArgs<string> args);
    }
}
