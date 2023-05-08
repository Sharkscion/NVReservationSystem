using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByFlightNumberView : IFormView
    {
        event EventHandler FlightNumberChanged;
        event EventHandler Submitted;

        public int FlightNumber { get; set; }
        void Display(IEnumerable<IFlight> flights);
        void DisplayNoFlights();
    }
}
