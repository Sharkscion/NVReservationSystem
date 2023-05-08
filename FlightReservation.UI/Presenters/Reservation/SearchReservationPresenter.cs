using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Common.Validators;
using FlightReservation.UI.Presenters.Reservation.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Presenters.Reservation
{
    internal class SearchReservationPresenter : ISearchReservationPresenter
    {
        private readonly ISearchReservationView _view;
        private readonly IReservationService _service;

        public SearchReservationPresenter(ISearchReservationView view, IReservationService service)
        {
            _service = service;

            _view = view;
            _view.PNRChanged += OnPNRChanged;
            _view.Submitted += OnSubmitted;
        }

        public void OnPNRChanged(object? source, EventArgs e)
        {
            bool isValid = ReservationValidator.IsBookingReferenceFormatValid(_view.PNR);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.PNR),
                    "PNR should be 6 uppercased-alphanumeric characters."
                );
            }
        }

        public void OnSubmitted(object? source, EventArgs e)
        {
            IReservation? reservation = _service.Find(_view.PNR);

            if (reservation == null)
            {
                _view.DisplayNoReservation();
            }
            else
            {
                _view.DisplayReservation(reservation);
            }
            _view.Reset();
        }
    }
}
