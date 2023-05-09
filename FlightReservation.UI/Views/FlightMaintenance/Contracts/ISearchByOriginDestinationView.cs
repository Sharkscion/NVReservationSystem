using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByOriginDestinationView : IFormView
    {
        #region Properties
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        #endregion

        #region Events
        event EventHandler DepartureStationChanged;
        event EventHandler ArrivalStationChanged;
        event EventHandler Submitted;
        #endregion

        #region Functions
        void Display(IEnumerable<IFlight> flights);
        void DisplayNoFlights();
        #endregion
    }
}
