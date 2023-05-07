using FlightReservation.Common.Validators;
using FlightReservation.Models.Contracts;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.Reservation.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Presenters.Reservation
{
    internal class SearchReservationPresenter : ISearchReservationPresenter
    {
        private readonly ISearchReservationView _view;
        private readonly IReservationService _service;

        public SearchReservationPresenter(ISearchReservationView view, IReservationService service)
        {
            _view = view;
            _service = service;
        }

        public void OnPNRChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = ReservationValidator.IsBookingReferenceFormatValid(args.Value);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.PNR),
                    "PNR should be 6 uppercased-alphanumeric characters."
                );
            }
        }

        public void OnSubmitted(IFormView source, SubmitEventArgs<string> args)
        {
            IReservation? reservation = _service.Find(PNR: args.Data);
            _view.DisplayReservation(reservation);
            _view.Reset();
        }
    }
}
