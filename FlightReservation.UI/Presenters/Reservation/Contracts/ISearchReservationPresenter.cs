using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.Reservation.Contracts
{
    internal interface ISearchReservationPresenter
    {
        public void OnPNRChanged(IFormView source, ChangeEventArgs<string> args);

        public void OnSubmitted(IFormView source, SubmitEventArgs<string> args);
    }
}
