using FlightReservation.Data.Contracts;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface IDisplayAllReservationsView : IFormView
    {
        event SubmitEventHandler<EventArgs> Submitted;

        void DisplayReservations(IEnumerable<IReservation> reservations);
    }
}
