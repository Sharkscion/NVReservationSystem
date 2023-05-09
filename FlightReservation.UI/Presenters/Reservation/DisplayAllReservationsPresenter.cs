using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.UI.Presenters.Reservation.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Presenters.Reservation
{
    internal class DisplayAllReservationsPresenter : IDisplayAllReservationsPresenter
    {
        #region Declarations
        private readonly IDisplayAllReservationsView _view;
        private readonly IReservationService _service;
        #endregion

        #region Constructors
        public DisplayAllReservationsPresenter(
            IDisplayAllReservationsView view,
            IReservationService service
        )
        {
            _service = service;

            _view = view;
            _view.Submitted += OnSubmitted;
        }
        #endregion

        #region Implementations of IDisplayAllReservationsPresenter
        public void OnSubmitted(object? source, EventArgs e)
        {
            IEnumerable<IReservation> reservations = _service.ViewAll();

            if (reservations.Count() > 0)
            {
                _view.DisplayReservations(reservations);
            }
            else
            {
                _view.DisplayNoReservations();
            }
        }
        #endregion
    }
}
