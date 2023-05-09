using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByFlightNumberView : IFormView
    {
        #region Properties
        public int FlightNumber { get; set; }
        #endregion

        #region Events
        event EventHandler FlightNumberChanged;
        event EventHandler Submitted;
        #endregion

        #region Functions
        void Display(IEnumerable<IFlight> flights);
        void DisplayNoFlights();
        #endregion
    }
}
