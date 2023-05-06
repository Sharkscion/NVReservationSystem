using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class AddFlightPage : BasePage, IAddFlightView
    {
        private const string TIME_FORMAT = "HH:mm";

        private bool _isFormValid;

        private string _airlineCode;
        private int _flightNumber;

        private string _departureStation;
        private string _arrivalStation;

        private TimeOnly _departureScheduledTime;
        private TimeOnly _arrivalScheduledTime;

        public event IFormView.InputChangedEventHandler<string> AirlineCodeChanged;
        public event IFormView.InputChangedEventHandler<int> FlightNumberChanged;
        public event IFormView.InputChangedEventHandler<string> ArrivalStationChanged;
        public event IFormView.InputChangedEventHandler<string> DepartureStationChanged;
        public event IFormView.InputChangedEventHandler<TimeOnly> ArrivalScheduledTimeChanged;
        public event IFormView.InputChangedEventHandler<TimeOnly> DepartureScheduledTimeChanged;
        public event IFormView.SubmitEventHandler<FlightEventArgs> Submitted;

        public AddFlightPage(string title)
            : base(title)
        {
            Reset();
        }

        public override void ShowContent()
        {
            getAirlineCode();
            getFlightNumber();
            getDepartureStationCode();
            getArrivalStationCode();
            getDepartureScheduledTime();
            getArrivalScheduledTime();

            displaySummary();
            submitForm();
        }

        private void getAirlineCode()
        {
            do
            {
                Console.Write("Airline Code: ");
                string? input = Console.ReadLine();

                _isFormValid = input is not null && input != string.Empty;
                if (!_isFormValid)
                {
                    SetAirlineCodeError("Please enter a value...");
                    continue;
                }

                OnAirlineCodeChanged(input);
            } while (!_isFormValid);
        }

        private void getFlightNumber()
        {
            do
            {
                Console.Write("Flight Number: ");
                string? input = Console.ReadLine();

                _isFormValid = int.TryParse(input, out int flightNumber);
                if (!_isFormValid)
                {
                    SetFlightNumberError("Please enter a numeric value...");
                    continue;
                }

                OnFlightNumberChanged(flightNumber);
            } while (!_isFormValid);
        }

        private void getDepartureStationCode()
        {
            do
            {
                Console.Write("Departure Station Code: ");
                string? input = Console.ReadLine();

                _isFormValid = input is not null && input != string.Empty;
                if (!_isFormValid)
                {
                    SetDepartureStationError("Please enter a value...");
                    continue;
                }

                OnDepartureStationChanged(input);
            } while (!_isFormValid);
        }

        private void getArrivalStationCode()
        {
            do
            {
                Console.Write("Arrival Station Code: ");
                string? input = Console.ReadLine();

                _isFormValid = input is not null && input != string.Empty;
                if (!_isFormValid)
                {
                    SetArrivalStationError("Please enter a value...");
                    continue;
                }

                _isFormValid = input != _departureStation;
                if (!_isFormValid)
                {
                    SetArrivalStationError(
                        "Arrival station cannot be the same as the departure station."
                    );
                    continue;
                }

                OnArrivalStationChanged(input);
            } while (!_isFormValid);
        }

        private void getDepartureScheduledTime()
        {
            do
            {
                Console.Write("Departure Scheduled Time [HH:mm]: ");
                string? input = Console.ReadLine();

                _isFormValid = TimeOnly.TryParseExact(input, TIME_FORMAT, out TimeOnly parsedValue);
                if (!_isFormValid)
                {
                    SetDepartureScheduledTimeError(
                        "Scheduled departure time should be in \"HH:mm\" format."
                    );
                    continue;
                }

                _isFormValid = parsedValue < _arrivalScheduledTime;
                if (!_isFormValid)
                {
                    SetDepartureScheduledTimeError(
                        "Scheduled departure time must be before the scheduled arrival time."
                    );
                    continue;
                }

                OnScheduledDepartureTimeChanged(parsedValue);
            } while (!_isFormValid);
        }

        private void getArrivalScheduledTime()
        {
            do
            {
                Console.Write("Arrival Scheduled Time [HH:mm]: ");
                string? input = Console.ReadLine();

                _isFormValid = TimeOnly.TryParseExact(input, TIME_FORMAT, out TimeOnly parsedValue);
                if (!_isFormValid)
                {
                    SetArrivalScheduledTimeError(
                        "Scheduled arrival time should be in \"HH:mm\" format."
                    );
                    continue;
                }

                _isFormValid = _departureScheduledTime < parsedValue;
                if (!_isFormValid)
                {
                    SetArrivalScheduledTimeError(
                        "Scheduled arrival time must be after the scheduled departure time."
                    );
                    continue;
                }

                OnScheduledArrivalTimeChanged(parsedValue);
            } while (!_isFormValid);
        }

        private void submitForm()
        {
            char option = getYesOrNoInput("Save flight");

            if (option == 'Y')
            {
                OnSubmitted();
            }
        }

        private void displaySummary()
        {
            ClearScreen();

            Console.WriteLine(Title);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Airline Code: {_airlineCode}");
            Console.WriteLine($"Flight Number: {_flightNumber}");
            Console.WriteLine($"Departure Station Code: {_departureStation}");
            Console.WriteLine($"Arrival Station Code: {_arrivalStation}");
            Console.WriteLine(
                $"Scheduled Departure Time: {_departureScheduledTime.ToString("HH:mm")}"
            );
            Console.WriteLine($"Scheduled Arrival Time: {_arrivalScheduledTime.ToString("HH:mm")}");
            Console.WriteLine("-------------------------------------------");
        }

        public void SetAirlineCodeError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void SetFlightNumberError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void SetArrivalStationError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void SetDepartureStationError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void SetArrivalScheduledTimeError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void SetDepartureScheduledTimeError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void Reset()
        {
            _isFormValid = true;

            _airlineCode = string.Empty;
            _flightNumber = 0;

            _departureStation = string.Empty;
            _arrivalStation = string.Empty;

            _departureScheduledTime = new TimeOnly(hour: 0, minute: 0);
            _arrivalScheduledTime = new TimeOnly(hour: 23, minute: 59);
        }

        private void OnAirlineCodeChanged(string value)
        {
            _airlineCode = value;
            AirlineCodeChanged?.Invoke(this, new ChangeEventArgs<string>(_airlineCode));
        }

        private void OnFlightNumberChanged(int value)
        {
            _flightNumber = value;
            FlightNumberChanged?.Invoke(this, new ChangeEventArgs<int>(_flightNumber));
        }

        private void OnDepartureStationChanged(string value)
        {
            _departureStation = value;
            DepartureStationChanged?.Invoke(this, new ChangeEventArgs<string>(_departureStation));
        }

        private void OnArrivalStationChanged(string value)
        {
            _arrivalStation = value;
            ArrivalStationChanged?.Invoke(this, new ChangeEventArgs<string>(_arrivalStation));
        }

        private void OnScheduledDepartureTimeChanged(TimeOnly value)
        {
            _departureScheduledTime = value;
            DepartureScheduledTimeChanged?.Invoke(
                this,
                new ChangeEventArgs<TimeOnly>(_departureScheduledTime)
            );
        }

        private void OnScheduledArrivalTimeChanged(TimeOnly value)
        {
            _arrivalScheduledTime = value;
            ArrivalScheduledTimeChanged?.Invoke(
                this,
                new ChangeEventArgs<TimeOnly>(_arrivalScheduledTime)
            );
        }

        private void OnSubmitted()
        {
            var args = new FlightEventArgs()
            {
                AirlineCode = _airlineCode,
                FlightNumber = _flightNumber,
                DepartureStation = _departureStation,
                ArrivalStation = _arrivalStation,
                DepartureScheduledTime = _departureScheduledTime,
                ArrivalScheduledTime = _arrivalScheduledTime
            };

            Submitted?.Invoke(this, args);
        }

        public void AlertError(string header, string message)
        {
            Console.WriteLine();
            Console.WriteLine("*****************************************");
            Console.WriteLine(header);
            Console.WriteLine(message);
            Console.WriteLine("*****************************************");

            Console.WriteLine();
        }
    }
}
