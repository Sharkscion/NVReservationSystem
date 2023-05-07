using FlightReservation.Models.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface IDisplayAllReservationsView
    {
        delegate void SubmitEventHandler<T>(IDisplayAllReservationsView sender, T args);
        event SubmitEventHandler<EventArgs> Submitted;

        void DisplayReservations(IEnumerable<IReservation> reservations);
    }
}
