using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Common.Validators;
using FlightReservation.Models.Reservation;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.Reservation.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Presenters.Reservation
{
    internal class CreateReservationPresenter : ICreateReservationPresenter
    {
        private readonly ICreateReservationView _view;
        private readonly IReservationService _reservationService;
        private readonly IFlightService _flightService;
        private readonly IFlight _flightModel;
        private readonly IPassenger _passengerModel;
        private readonly IReservation _reservationModel;

        public CreateReservationPresenter(
            ICreateReservationView view,
            IReservationService reservationService,
            IFlightService flightService,
            IReservation reservationModel,
            IFlight flightModel,
            IPassenger passengerModel
        )
        {
            _reservationService = reservationService;
            _flightService = flightService;

            _flightModel = flightModel;
            _passengerModel = passengerModel;
            _reservationModel = reservationModel;

            _view = view;
            _view.AirlineCodeChanged += OnAirlineCodeChanged;
            _view.FlightNumberChanged += OnFlightNumberChanged;
            _view.FlightDateChanged += OnFlightDateChanged;

            _view.FirstNameChanged += OnFirstNameChanged;
            _view.LastNameChanged += OnLastNameChanged;
            _view.BirthDateChanged += OnBirthDateChanged;

            _view.FlightSearched += OnFlightSearched;
            _view.Submitted += OnSubmitted;
        }

        public void OnAirlineCodeChanged(object? source, EventArgs e)
        {
            bool isValid = FlightValidator.IsAirlineCodeValid(_view.AirlineCode);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.AirlineCode),
                    "Airline code must be 2-3 uppercased-alphanumeric characters."
                );
            }
        }

        public void OnBirthDateChanged(object? source, EventArgs e)
        {
            bool isValid = PassengerValidator.IsBirthDateValid(_view.BirthDate);
            if (!isValid)
            {
                _view.SetFieldError("BirthDate", "Passenger should at least be 16 days old.");
            }
        }

        public void OnFirstNameChanged(object? source, EventArgs e)
        {
            bool isValid = PassengerValidator.IsNameValid(_view.FirstName);
            if (!isValid)
            {
                _view.SetFieldError("FirstName", "Name should at least be 20 letters.");
            }
        }

        public void OnFlightDateChanged(object? source, EventArgs e)
        {
            bool isValid = ReservationValidator.IsFlightDateValid(_view.FlightDate);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.FlightDate),
                    "Flight date cannot not be past-dated."
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
                    "Flight number cannot be an integer between 1 and 9999."
                );
            }
        }

        public void OnFlightSearched(object? source, EventArgs e)
        {
            IEnumerable<IFlight> availableFlights = _flightService.FindAvailableFlightsOn(
                flightDate: _view.FlightDate,
                airlineCode: _view.AirlineCode,
                flightNumber: _view.FlightNumber
            );

            if (availableFlights.Count() > 0)
            {
                _view.DisplayAvailableFlights(availableFlights);
                _view.ShowFlightSelection(availableFlights);
            }
            else
            {
                _view.DisplayNoFlights();
            }
        }

        public void OnLastNameChanged(object? source, EventArgs e)
        {
            bool isValid = PassengerValidator.IsNameValid(_view.LastName);
            if (!isValid)
            {
                _view.SetFieldError("LastName", "Name should at least be 20 letters.");
            }
        }

        public void OnSubmitted(object? source, ReservationEventArgs args)
        {
            string errorHeader = "Failed to create reservation.";

            try
            {
                var flightInfo = _flightModel.CreateFrom(
                    airlineCode: args.FlightInfo.AirlineCode,
                    flightNumber: args.FlightInfo.FlightNumber,
                    departureStation: args.FlightInfo.DepartureStation,
                    arrivalStation: args.FlightInfo.ArrivalStation,
                    departureScheduledTime: args.FlightInfo.DepartureScheduledTime,
                    arrivalScheduledTime: args.FlightInfo.ArrivalScheduledTime
                );

                var passengers = args.Passengers.Select(
                    (item) =>
                        _passengerModel.CreateFrom(
                            firstName: item.FirstName,
                            lastName: item.LastName,
                            birthDate: item.BirthDate
                        )
                );

                var reservation = _reservationModel.CreateFrom(
                    flightDate: args.FlightDate,
                    flightInfo: flightInfo,
                    passengers: passengers
                );

                string bookingReference = _reservationService.Create(reservation);
                _view.DisplayBookingConfirmation(bookingReference);
            }
            catch (InvalidPNRException e)
            {
                _view.AlertError(errorHeader, e.Message);
            }
            catch (FlightDoesNotExistException e)
            {
                _view.AlertError(errorHeader, e.Message);
            }
            catch (MoreThanMaxPassengerCountException e)
            {
                _view.AlertError(errorHeader, e.Message);
            }
            catch (Exception e)
            {
                _view.AlertError(errorHeader, e.Message);
            }
            finally
            {
                _view.Reset();
            }
        }
    }
}
