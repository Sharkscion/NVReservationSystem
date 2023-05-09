using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class AddFlightPage : BasePage, IAddFlightView
    {
        #region Declarations
        private const string TIME_FORMAT = "HH:mm";

        private bool _isFormValid;
        private string _airlineCode;
        private int _flightNumber;

        private string _departureStation;
        private string _arrivalStation;

        private TimeOnly _departureScheduledTime;
        private TimeOnly _arrivalScheduledTime;
        #endregion

        #region Properties
        public string AirlineCode
        {
            get { return _airlineCode; }
            set
            {
                _airlineCode = value;
                onAirlineCodeChanged();
            }
        }

        public int FlightNumber
        {
            get { return _flightNumber; }
            set
            {
                _flightNumber = value;
                onFlightNumberChanged();
            }
        }
        public string DepartureStation
        {
            get { return _departureStation; }
            set
            {
                _departureStation = value;
                onDepartureStationChanged();
            }
        }

        public string ArrivalStation
        {
            get { return _arrivalStation; }
            set
            {
                _arrivalStation = value;
                onArrivalStationChanged();
            }
        }

        public TimeOnly DepartureScheduledTime
        {
            get { return _departureScheduledTime; }
            set
            {
                _departureScheduledTime = value;
                onScheduledDepartureTimeChanged();
            }
        }

        public TimeOnly ArrivalScheduledTime
        {
            get { return _arrivalScheduledTime; }
            set
            {
                _arrivalScheduledTime = value;
                onScheduledArrivalTimeChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }
        #endregion

        #region Events
        public event EventHandler AirlineCodeChanged;
        public event EventHandler FlightNumberChanged;
        public event EventHandler ArrivalStationChanged;
        public event EventHandler DepartureStationChanged;
        public event EventHandler ArrivalScheduledTimeChanged;
        public event EventHandler DepartureScheduledTimeChanged;
        public event EventHandler Submitted;
        #endregion

        #region Constructors
        public AddFlightPage(string title)
            : base(title)
        {
            Reset();
        }
        #endregion

        #region Implementation of BasePage Methods
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
        #endregion

        #region Implementations of IFormView
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
        #endregion

        #region Event Invocation Methods
        private void onAirlineCodeChanged()
        {
            AirlineCodeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onFlightNumberChanged()
        {
            FlightNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onDepartureStationChanged()
        {
            DepartureStationChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onArrivalStationChanged()
        {
            ArrivalStationChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onScheduledDepartureTimeChanged()
        {
            DepartureScheduledTimeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onScheduledArrivalTimeChanged()
        {
            ArrivalScheduledTimeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Private Methods
        private void getAirlineCode()
        {
            do
            {
                Console.Write("Airline Code: ");
                string? input = Console.ReadLine();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError(nameof(AirlineCode), "Please enter a value...");
                    continue;
                }

                AirlineCode = input;
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

                FlightNumber = flightNumber;
            } while (!_isFormValid);
        }

        private void getDepartureStationCode()
        {
            do
            {
                Console.Write("Departure Station Code: ");
                string? input = Console.ReadLine();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError(nameof(DepartureStation), "Please enter a value...");
                    continue;
                }

                DepartureStation = input;
            } while (!_isFormValid);
        }

        private void getArrivalStationCode()
        {
            do
            {
                Console.Write("Arrival Station Code: ");
                string? input = Console.ReadLine();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError(nameof(ArrivalStation), "Please enter a value...");
                    continue;
                }

                ArrivalStation = input;
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

                DepartureScheduledTime = parsedValue;
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

                ArrivalScheduledTime = parsedValue;
            } while (!_isFormValid);
        }

        private void submitForm()
        {
            char option = getYesOrNoInput("Save flight");

            if (option == 'Y')
            {
                onSubmitted();
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
        #endregion
    }
}
