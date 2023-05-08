using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Common.Validators;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class SearchByOriginDestinationPresenter : ISearchByOriginDestinationPresenter
    {
        private readonly ISearchByOriginDestinationView _view;
        private readonly IFlightService _service;

        public SearchByOriginDestinationPresenter(
            ISearchByOriginDestinationView view,
            IFlightService service
        )
        {
            _service = service;

            _view = view;
            _view.DepartureStationChanged += OnDepartureStationChanged;
            _view.ArrivalStationChanged += OnArrivalStationChanged;
            _view.Submitted += OnSubmitted;
        }

        private void _view_ArrivalStationChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnArrivalStationChanged(object? source, EventArgs e)
        {
            bool isValid = FlightValidator.IsStationFormatValid(_view.ArrivalStation);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station code must be 3 uppercased-alphanumeric characters."
                );
                return;
            }

            if (_view.ArrivalStation == _view.DepartureStation)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station must not be the same as the departure station."
                );
            }
        }

        public void OnDepartureStationChanged(object? source, EventArgs e)
        {
            bool isValid = FlightValidator.IsStationFormatValid(_view.DepartureStation);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station code must be 3 uppercased-alphanumeric characters."
                );
                return;
            }

            if (_view.DepartureStation == _view.ArrivalStation)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station must not be the same as the arrival station."
                );
            }
        }

        public void OnSubmitted(object? source, EventArgs e)
        {
            IEnumerable<IFlight> flights = _service.FindAllHaving(
                origin: _view.DepartureStation,
                destination: _view.ArrivalStation
            );

            if (flights.Count() > 0)
            {
                _view.Display(flights);
            }
            else
            {
                _view.DisplayNoFlights();
            }

            _view.Reset();
        }
    }
}
