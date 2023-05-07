﻿using FlightReservation.Models.Contracts;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Presenters.Reservation.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Presenters.Reservation
{
    internal class DisplayAllReservationsPresenter : IDisplayAllReservationsPresenter
    {
        private readonly IDisplayAllReservationsView _view;
        private readonly IReservationService _service;

        public DisplayAllReservationsPresenter(
            IDisplayAllReservationsView view,
            IReservationService service
        )
        {
            _view = view;
            _service = service;
        }

        public void OnSubmitted(IDisplayAllReservationsView source, EventArgs args)
        {
            IEnumerable<IReservation> reservations = _service.ViewAll();
            _view.DisplayReservations(reservations);
        }
    }
}
