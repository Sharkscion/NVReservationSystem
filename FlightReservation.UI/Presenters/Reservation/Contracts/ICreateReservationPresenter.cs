using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.Reservation.Contracts
{
    internal interface ICreateReservationPresenter
    {
        public void OnFlightDateChanged(object? source, EventArgs e);
        public void OnAirlineCodeChanged(object? source, EventArgs e);
        public void OnFlightNumberChanged(object? source, EventArgs e);
        public void OnFirstNameChanged(object? source, EventArgs e);
        public void OnLastNameChanged(object? source, EventArgs e);
        public void OnBirthDateChanged(object? source, EventArgs e);
        public void OnFlightSearched(object? source, EventArgs e);
        public void OnSubmitted(object? source, ReservationEventArgs args);
    }
}
