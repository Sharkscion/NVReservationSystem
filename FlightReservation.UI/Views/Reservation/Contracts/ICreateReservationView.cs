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

        public DateTime FlightDate { get; set; }
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }

        void DisplayBookingConfirmation(string bookingReference);
        void DisplayAvailableFlights(IEnumerable<IFlight> flights);
    }
}
