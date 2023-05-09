using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByAirlineCodeView : IFormView
    {
        #region Properties
        public string AirlineCode { get; set; }

        #endregion

        #region Events
        event EventHandler AirlineCodeChanged;
        event EventHandler Submitted;
        #endregion

        #region Functions
        void Display(IEnumerable<IFlight> flights);
        void DisplayNoFlights();
        #endregion
    }
}
