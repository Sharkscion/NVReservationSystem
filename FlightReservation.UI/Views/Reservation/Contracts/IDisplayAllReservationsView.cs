using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface IDisplayAllReservationsView
    {
        #region Events
        event EventHandler Submitted;

        #endregion

        #region Functions
        void DisplayReservations(IEnumerable<IReservation> reservations);
        void DisplayNoReservations();
        #endregion
    }
}
