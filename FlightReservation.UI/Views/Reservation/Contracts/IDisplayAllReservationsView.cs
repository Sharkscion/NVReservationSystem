using FlightReservation.Models.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface IDisplayAllReservationsView
    {
        event EventHandler Submitted;

        void DisplayReservations(IEnumerable<IReservation> reservations);
        void DisplayNoReservations();
    }
}
