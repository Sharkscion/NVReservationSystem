using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface ISearchReservationView : IFormView
    {
        #region Properties
        public string PNR { get; set; }

        #endregion

        #region Events
        event EventHandler PNRChanged;
        event EventHandler Submitted;
        #endregion

        #region Functions
        void DisplayReservation(IReservation reservation);
        void DisplayNoReservation();
        #endregion
    }
}
