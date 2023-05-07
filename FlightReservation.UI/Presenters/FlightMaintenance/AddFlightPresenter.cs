using FlightReservation.Common.Validators;
using FlightReservation.Models.Flight;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class AddFlightPresenter : IAddFlightPresenter
    {
        private readonly IAddFlightView _view;
        private readonly IFlightService _service;

        public AddFlightPresenter(IAddFlightView view, IFlightService service)
        {
            _view = view;
            _service = service;
        }

        public void OnAirlineCodeChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = FlightValidator.IsAirlineCodeValid(args.Value);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.AirlineCode),
                    "Airline code must be 2-3 uppercased-alphanumeric characters "
                        + "where a numeric character could appear at most once."
                );
            }
        }

        public void OnArrivalStationChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isFormatValid = FlightValidator.IsStationFormatValid(args.Value);
            if (!isFormatValid)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station must be exactly 3 uppercased-alphanumeric characters "
                        + "where the first character is a letter."
                );
                return;
            }

            if (_view.DepartureStation == args.Value)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station must not be the same as the Departure Station."
                );
            }
        }

        public void OnArrivalScheduledTimeChanged(IFormView source, ChangeEventArgs<TimeOnly> args)
        {
            if (_view.DepartureScheduledTime > args.Value)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalScheduledTime),
                    "Arrival scheduled time must be after the departure scheduled time."
                );
            }
        }

        public void OnDepartureScheduledTimeChanged(
            IFormView source,
            ChangeEventArgs<TimeOnly> args
        )
        {
            if (args.Value > _view.ArrivalScheduledTime)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureScheduledTime),
                    "Departure scheduled time must be before the arrival scheduled time."
                );
            }
        }

        public void OnDepartureStationChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isFormatValid = FlightValidator.IsStationFormatValid(args.Value);
            if (!isFormatValid)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station must be exactly 3 uppercased-alphanumeric characters "
                        + "where the first character is a letter."
                );
                return;
            }

            if (_view.ArrivalStation == args.Value)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station must not be the same as the arrival station."
                );
            }
        }

        public void OnFlightNumberChanged(IFormView source, ChangeEventArgs<int> args)
        {
            bool isValid = FlightValidator.IsFlightNumberValid(args.Value);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.FlightNumber),
                    "Flight number must be an integer between 1 and 9999."
                );
            }
        }

        public void OnSubmitted(IFormView source, FlightEventArgs args)
        {
            try
            {
                var flight = new FlightModel(
                    airlineCode: args.AirlineCode,
                    flightNumber: args.FlightNumber,
                    departureStation: args.DepartureStation,
                    arrivalStation: args.ArrivalStation,
                    departureScheduledTime: args.DepartureScheduledTime,
                    arrivalScheduledTime: args.ArrivalScheduledTime
                );

                _service.Create(flight);
            }
            catch (InvalidAirlineCodeException e)
            {
                _view.SetFieldError(nameof(_view.AirlineCode), e.Message);
            }
            catch (InvalidFlightNumberException e)
            {
                _view.SetFieldError(nameof(_view.FlightNumber), e.Message);
            }
            catch (InvalidMarketPairException e)
            {
                _view.SetFieldError(e.ParamName, message: e.Message);
            }
            catch (InvalidStationFormatException e)
            {
                _view.SetFieldError(e.ParamName, message: e.Message);
            }
            catch (InvalidFlightTimeException e)
            {
                _view.SetFieldError(e.ParamName, message: e.Message);
            }
            catch (DuplicateFlightException e)
            {
                _view.AlertError(header: "Failed to add a flight.", message: e.Message);
            }
            catch (Exception e)
            {
                _view.AlertError(header: "Failed to add a flight.", message: e.Message);
            }
            finally
            {
                _view.Reset();
            }
        }
    }
}
