using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Presenters.Reservation.Contracts
{
    internal interface ICreateReservationPresenter
    {
        public void OnFlightDateChanged(IFormView source, ChangeEventArgs<DateTime> args);
        public void OnAirlineCodeChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnFlightNumberChanged(IFormView source, ChangeEventArgs<int> args);
        public void OnFirstNameChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnLastNameChanged(IFormView source, ChangeEventArgs<string> args);
        public void OnBirthDateChanged(IFormView source, ChangeEventArgs<DateTime> args);

        public void OnFlightSearched(IFormView source, SearchAvailableFlightEventArgs args);
        public void OnSubmitted(IFormView source, ReservationEventArgs args);
    }
}
