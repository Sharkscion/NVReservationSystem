using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByAirlineCodeView : IFormView
    {
        event EventHandler AirlineCodeChanged;
        event EventHandler Submitted;

        public string AirlineCode { get; set; }
        void Display(IEnumerable<IFlight> flights);
        void DisplayNoFlights();
    }
}
