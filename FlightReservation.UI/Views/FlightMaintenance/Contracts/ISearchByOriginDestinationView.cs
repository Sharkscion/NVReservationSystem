using FlightReservation.Models.Contracts;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByOriginDestinationView : IFormView
    {
        event EventHandler DepartureStationChanged;
        event EventHandler ArrivalStationChanged;
        event EventHandler Submitted;

        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }

        void Display(IEnumerable<IFlight> flights);
        void DisplayNoFlights();
    }
}
