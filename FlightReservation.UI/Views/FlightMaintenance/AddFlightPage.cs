using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class AddFlightPage : BasePage, IAddFlightView
    {
        private const string TIME_FORMAT = "HH:mm";

        private bool _isFormValid;

        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public TimeOnly DepartureScheduledTime { get; set; }
        public TimeOnly ArrivalScheduledTime { get; set; }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

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
                    SetFieldError(nameof(AirlineCode), "Please enter a value...");
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
                    SetFieldError(nameof(FlightNumber), "Please enter a numeric value...");
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
                    SetFieldError(nameof(DepartureStation), "Please enter a value...");
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
                    SetFieldError(nameof(ArrivalStation), "Please enter a value...");
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
                    SetFieldError(
                        nameof(DepartureScheduledTime),
                        "Scheduled departure time should be in \"HH:mm\" format."
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
                    SetFieldError(
                        nameof(ArrivalScheduledTime),
                        "Scheduled arrival time should be in \"HH:mm\" format."
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
            Console.WriteLine($"Airline Code: {AirlineCode}");
            Console.WriteLine($"Flight Number: {FlightNumber}");
            Console.WriteLine($"Departure Station Code: {DepartureStation}");
            Console.WriteLine($"Arrival Station Code: {ArrivalStation}");
            Console.WriteLine(
                $"Scheduled Departure Time: {DepartureScheduledTime.ToString("HH:mm")}"
            );
            Console.WriteLine($"Scheduled Arrival Time: {ArrivalScheduledTime.ToString("HH:mm")}");
            Console.WriteLine("-------------------------------------------");
        }

        public void Reset()
        {
            _isFormValid = true;

            AirlineCode = string.Empty;
            FlightNumber = 0;

            DepartureStation = string.Empty;
            ArrivalStation = string.Empty;

            DepartureScheduledTime = new TimeOnly(hour: 0, minute: 0);
            ArrivalScheduledTime = new TimeOnly(hour: 23, minute: 59);
        }

        private void OnAirlineCodeChanged(string value)
        {
            AirlineCode = value;
            AirlineCodeChanged?.Invoke(this, new ChangeEventArgs<string>(AirlineCode));
        }

        private void OnFlightNumberChanged(int value)
        {
            FlightNumber = value;
            FlightNumberChanged?.Invoke(this, new ChangeEventArgs<int>(FlightNumber));
        }

        private void OnDepartureStationChanged(string value)
        {
            DepartureStation = value;
            DepartureStationChanged?.Invoke(this, new ChangeEventArgs<string>(DepartureStation));
        }

        private void OnArrivalStationChanged(string value)
        {
            ArrivalStation = value;
            ArrivalStationChanged?.Invoke(this, new ChangeEventArgs<string>(ArrivalStation));
        }

        private void OnScheduledDepartureTimeChanged(TimeOnly value)
        {
            DepartureScheduledTime = value;
            DepartureScheduledTimeChanged?.Invoke(
                this,
                new ChangeEventArgs<TimeOnly>(DepartureScheduledTime)
            );
        }

        private void OnScheduledArrivalTimeChanged(TimeOnly value)
        {
            ArrivalScheduledTime = value;
            ArrivalScheduledTimeChanged?.Invoke(
                this,
                new ChangeEventArgs<TimeOnly>(ArrivalScheduledTime)
            );
        }

        private void OnSubmitted()
        {
            var args = new FlightEventArgs(
                AirlineCode,
                FlightNumber,
                ArrivalStation,
                DepartureStation,
                ArrivalScheduledTime,
                DepartureScheduledTime
            );
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

        public void SetFieldError(string paramName, string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
