using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface ICreateReservationView : IFormView
    {
        #region Properties
        public DateTime FlightDate { get; set; }
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        #endregion

        #region Events
        event EventHandler FlightDateChanged;
        event EventHandler AirlineCodeChanged;
        event EventHandler FlightNumberChanged;
        event EventHandler FirstNameChanged;
        event EventHandler LastNameChanged;
        event EventHandler BirthDateChanged;

        event EventHandler FlightSearched;
        event EventHandler<ReservationEventArgs> Submitted;
        #endregion

        #region Functions
        void DisplayBookingConfirmation(string bookingReference);
        void DisplayAvailableFlights(IEnumerable<IFlight> flights);
        void ShowFlightSelection(IEnumerable<IFlight> flights);
        void DisplayNoFlights();
        #endregion
    }
}
