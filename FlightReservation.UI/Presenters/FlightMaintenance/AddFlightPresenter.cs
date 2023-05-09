using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Common.Validators;
using FlightReservation.Models.Flight;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class AddFlightPresenter : IAddFlightPresenter
    {
        #region Declarations
        private readonly IAddFlightView _view;
        private readonly IFlightService _service;
        private readonly IFlight _model;
        #endregion

        #region Constructors
        public AddFlightPresenter(IAddFlightView view, IFlightService service, IFlight model)
        {
            _model = model;
            _service = service;

            _view = view;
            _view.AirlineCodeChanged += OnAirlineCodeChanged;
            _view.FlightNumberChanged += OnFlightNumberChanged;
            _view.DepartureStationChanged += OnDepartureStationChanged;
            _view.ArrivalStationChanged += OnArrivalStationChanged;
            _view.DepartureScheduledTimeChanged += OnDepartureScheduledTimeChanged;
            _view.ArrivalScheduledTimeChanged += OnArrivalScheduledTimeChanged;
            _view.Submitted += OnSubmitted;
        }
        #endregion

        #region Implementations of IAddFlightPresenter
        public void OnAirlineCodeChanged(object? source, EventArgs e)
        {
            bool isValid = FlightValidator.IsAirlineCodeValid(_view.AirlineCode);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.AirlineCode),
                    "Airline code must be 2-3 uppercased-alphanumeric characters "
                        + "where a numeric character could appear at most once."
                );
            }
        }

        public void OnArrivalStationChanged(object? source, EventArgs e)
        {
            bool isFormatValid = FlightValidator.IsStationFormatValid(_view.ArrivalStation);
            if (!isFormatValid)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station must be exactly 3 uppercased-alphanumeric characters "
                        + "where the first character is a letter."
                );
                return;
            }

            if (_view.DepartureStation == _view.ArrivalStation)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station must not be the same as the departure station."
                );
            }
        }

        public void OnArrivalScheduledTimeChanged(object? source, EventArgs e)
        {
            if (_view.DepartureScheduledTime > _view.ArrivalScheduledTime)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalScheduledTime),
                    "Arrival scheduled time must be after the departure scheduled time."
                );
            }
        }

        public void OnDepartureScheduledTimeChanged(object? source, EventArgs e)
        {
            if (_view.DepartureScheduledTime > _view.ArrivalScheduledTime)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureScheduledTime),
                    "Departure scheduled time must be before the arrival scheduled time."
                );
            }
        }

        public void OnDepartureStationChanged(object? source, EventArgs e)
        {
            bool isFormatValid = FlightValidator.IsStationFormatValid(_view.DepartureStation);
            if (!isFormatValid)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station must be exactly 3 uppercased-alphanumeric characters "
                        + "where the first character is a letter."
                );
                return;
            }

            if (_view.ArrivalStation == _view.DepartureStation)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station must not be the same as the arrival station."
                );
            }
        }

        public void OnFlightNumberChanged(object? source, EventArgs e)
        {
            bool isValid = FlightValidator.IsFlightNumberValid(_view.FlightNumber);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.FlightNumber),
                    "Flight number must be an integer between 1 and 9999."
                );
            }
        }

        public void OnSubmitted(object? source, EventArgs e)
        {
            try
            {
                var flight = _model.CreateFrom(
                    airlineCode: _view.AirlineCode,
                    flightNumber: _view.FlightNumber,
                    departureStation: _view.DepartureStation,
                    arrivalStation: _view.ArrivalStation,
                    departureScheduledTime: _view.DepartureScheduledTime,
                    arrivalScheduledTime: _view.ArrivalScheduledTime
                );

                _service.Create(flight);
            }
            catch (DuplicateFlightException ex)
            {
                _view.AlertError(header: "Failed to add a flight.", message: ex.Message);
            }
            catch (Exception ex)
            {
                _view.AlertError(header: "Failed to add a flight.", message: ex.Message);
            }
            finally
            {
                _view.Reset();
            }
        }
        #endregion
    }
}
