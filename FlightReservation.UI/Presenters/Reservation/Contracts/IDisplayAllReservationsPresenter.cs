using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.Reservation.Contracts
{
    internal interface IDisplayAllReservationsPresenter
    {
        public void OnSubmitted(IFormView source, EventArgs args);
    }
}
