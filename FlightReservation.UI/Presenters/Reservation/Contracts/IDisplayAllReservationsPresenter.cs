using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Presenters.Reservation.Contracts
{
    internal interface IDisplayAllReservationsPresenter
    {
        public void OnSubmitted(IDisplayAllReservationsView source, EventArgs args);
    }
}
