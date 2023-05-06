using FlightReservation.Common.Validators;
using FlightReservation.Data.Flight;
using FlightReservation.Data.Passenger;
using FlightReservation.Data.Reservation;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.Reservation.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Presenters.Reservation
{
    internal class CreateReservationPresenter : ICreateReservationPresenter
    {
        private readonly ICreateReservationView _view;
        private readonly IReservationService _reservationService;
        private readonly IFlightService _flightService;

        public CreateReservationPresenter(
            ICreateReservationView view,
            IReservationService reservationService,
            IFlightService flightService
        )
        {
            _view = view;
            _reservationService = reservationService;
            _flightService = flightService;
        }

        public void OnAirlineCodeChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = FlightValidator.IsAirlineCodeValid(args.Value);
            if (!isValid)
            {
                _view.SetAirlineCodeError(
                    "Airline code must be 2-3 uppercased-alphanumeric characters."
                );
            }
        }

        public void OnBirthDateChanged(IFormView source, ChangeEventArgs<DateTime> args)
        {
            bool isValid = PassengerValidator.IsBirthDateValid(args.Value);
            if (!isValid)
            {
                _view.SetBirthDateError("Passenger should at least be 16 days old.");
            }
        }

        public void OnFirstNameChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = PassengerValidator.IsNameValid(args.Value);
            if (!isValid)
            {
                _view.SetFirstNameError("Name should at least be 20 letters.");
            }
        }

        public void OnFlightDateChanged(IFormView source, ChangeEventArgs<DateTime> args)
        {
            bool isValid = ReservationValidator.IsFlightDateValid(args.Value);
            if (!isValid)
            {
                _view.SetFlightDateError("Flight date cannot not be past-dated.");
            }
        }

        public void OnFlightNumberChanged(IFormView source, ChangeEventArgs<int> args)
        {
            bool isValid = FlightValidator.IsFlightNumberValid(args.Value);
            if (!isValid)
            {
                _view.SetFlightNumberError(
                    "Flight number cannot be an integer between 1 and 9999."
                );
            }
        }

        public void OnFlightSearched(IFormView source, SearchAvailableFlightEventArgs args)
        {
            var availableFlights = _flightService.FindAvailableFlightsOn(
                flightDate: args.FlightDate,
                airlineCode: args.AirlineCode,
                flightNumber: args.FlightNumber
            );
            _view.DisplayAvailableFlights(availableFlights);
        }

        public void OnLastNameChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = PassengerValidator.IsNameValid(args.Value);
            if (!isValid)
            {
                _view.SetFirstNameError("Name should at least be 20 letters.");
            }
        }

        public void OnSubmitted(IFormView source, ReservationEventArgs args)
        {
            string errorHeader = "Failed to create reservation.";

            try
            {
                var flightInfo = new FlightModel(
                    airlineCode: args.FlightInfo.AirlineCode,
                    flightNumber: args.FlightInfo.FlightNumber,
                    departureStation: args.FlightInfo.DepartureStation,
                    arrivalStation: args.FlightInfo.ArrivalStation,
                    departureScheduledTime: args.FlightInfo.DepartureScheduledTime,
                    arrivalScheduledTime: args.FlightInfo.ArrivalScheduledTime
                );

                var passengers = args.Passengers.Select(
                    (item) =>
                        new PassengerModel(
                            firstName: item.FirstName,
                            lastName: item.LastName,
                            birthDate: item.BirthDate
                        )
                );

                var reservation = new ReservationModel(
                    flightDate: args.FlightDate,
                    flightInfo: flightInfo,
                    passengers: passengers
                );

                string bookingReference = _reservationService.Create(reservation);
                _view.DisplayBookingConfirmation(bookingReference);
            }
            catch (InvalidFlightDateException e)
            {
                _view.SetFlightDateError(e.Message);
            }
            catch (InvalidNameException e)
            {
                _view.AlertError(errorHeader, "A passenger contains an invalid name." + e.Message);
            }
            catch (AgeLimitException e)
            {
                _view.AlertError(errorHeader, e.Message);
            }
            catch (InvalidPNRException e)
            {
                _view.AlertError(errorHeader, e.Message);
            }
            catch (FlightDoesNotExistException e)
            {
                _view.AlertError(errorHeader, e.Message);
            }
            catch (MaxPassengerCountReachedException e)
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
