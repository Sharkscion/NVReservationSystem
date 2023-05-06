﻿using FlightReservation.Common.Validators;
using FlightReservation.Data.Flight;
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
                _view.SetAirlineCodeError(
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
                _view.SetArrivalStationError(
                    "Arrival station must be exactly 3 uppercased-alphanumeric characters "
                        + "where the first character is a letter."
                );
            }
        }

        public void OnArrivalScheduledTimeChanged(
            IFormView source,
            ChangeEventArgs<TimeOnly> args
        ) { }

        public void OnDepartureScheduledTimeChanged(
            IFormView source,
            ChangeEventArgs<TimeOnly> args
        ) { }

        public void OnDepartureStationChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isFormatValid = FlightValidator.IsStationFormatValid(args.Value);
            if (!isFormatValid)
            {
                _view.SetArrivalStationError(
                    "Departure station must be exactly 3 uppercased-alphanumeric characters "
                        + "where the first character is a letter."
                );
            }
        }

        public void OnFlightNumberChanged(IFormView source, ChangeEventArgs<int> args)
        {
            bool isValid = FlightValidator.IsFlightNumberValid(args.Value);
            if (!isValid)
            {
                _view.SetFlightNumberError("Flight number must be an integer between 1 and 9999.");
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
                _view.SetAirlineCodeError(e.Message);
            }
            catch (InvalidFlightNumberException e)
            {
                _view.SetFlightNumberError(e.Message);
            }
            catch (InvalidMarketPairException e)
            {
                dynamicallyCallViewErrorMethod(paramName: e.ParamName, message: e.Message);
            }
            catch (InvalidStationFormatException e)
            {
                dynamicallyCallViewErrorMethod(paramName: e.ParamName, message: e.Message);
            }
            catch (InvalidFlightTimeException e)
            {
                dynamicallyCallViewErrorMethod(paramName: e.ParamName, message: e.Message);
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

        private void dynamicallyCallViewErrorMethod(string paramName, string message)
        {
            var type = Type.GetType(nameof(IAddFlightView));
            var viewErrorMethod = type?.GetMethod($"Set{paramName}Error");

            object[] parameters = new object[] { message };
            viewErrorMethod?.Invoke(_view, parameters);
        }
    }
}
