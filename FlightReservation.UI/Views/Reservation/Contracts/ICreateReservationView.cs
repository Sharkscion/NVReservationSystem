using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface ICreateReservationView : IFormView
    {
        event InputChangedEventHandler<DateTime> FlightDateChanged;
        event InputChangedEventHandler<string> AirlineCodeChanged;
        event InputChangedEventHandler<int> FlightNumberChanged;
        event InputChangedEventHandler<string> FirstNameChanged;
        event InputChangedEventHandler<string> LastNameChanged;
        event InputChangedEventHandler<DateTime> BirthDateChanged;

        event SubmitEventHandler<SearchAvailableFlightEventArgs> FlightSearched;
        event SubmitEventHandler<ReservationEventArgs> Submitted;

        void SetFlightDateError(string message);
        void SetAirlineCodeError(string message);
        void SetFlightNumberError(string message);
        void SetFirstNameError(string message);
        void SetLastNameError(string message);
        void SetBirthDateError(string message);
        void AlertError(string header, string message);
        void DisplayBookingConfirmation(string bookingReference);
        void DisplayAvailableFlights(IEnumerable<IFlight> flights);
    }
}
