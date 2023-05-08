using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;
using FlightReservation.UI.Views.Utilities;
using System.Globalization;

namespace FlightReservation.UI.Views.Reservation
{
    internal class CreateReservationPage : BasePage, ICreateReservationView
    {
        private const int MAX_PAX_COUNT = 5;
        private const string DATE_FORMAT = "MM/dd/yyyy";

        private int _paxCount;
        private CultureInfo _dateCulture;
        private List<PassengerEventArgs> _passengers;
        private FlightEventArgs _selectedFlight;

        private bool _isFormValid;

        private string _airlineCode;
        private int _flightNumber;
        private DateTime _flightDate;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;

        public DateTime FlightDate
        {
            get { return _flightDate; }
            set
            {
                _flightDate = value;
                OnFlightDateChanged();
            }
        }
        public string AirlineCode
        {
            get { return _airlineCode; }
            set
            {
                _airlineCode = value;
                OnAirlineCodeChanged();
            }
        }

        public int FlightNumber
        {
            get { return _flightNumber; }
            set
            {
                _flightNumber = value;
                OnFlightNumberChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnFirstNameChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnLastNameChanged();
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnBirthDateChanged();
            }
        }

        public CreateReservationPage(string title)
            : base(title)
        {
            _dateCulture = new CultureInfo("en-US");
            Reset();
        }

        public event EventHandler FlightDateChanged;
        public event EventHandler AirlineCodeChanged;
        public event EventHandler FlightNumberChanged;
        public event EventHandler FirstNameChanged;
        public event EventHandler LastNameChanged;
        public event EventHandler BirthDateChanged;

        public event EventHandler FlightSearched;
        public event EventHandler<ReservationEventArgs> Submitted;

        public void AlertError(string header, string message)
        {
            _isFormValid = false;

            Console.WriteLine();
            Console.WriteLine("*****************************************");
            Console.WriteLine(header);
            Console.WriteLine(message);
            Console.WriteLine("*****************************************");
            Console.WriteLine();
        }

        public void DisplayBookingConfirmation(string bookingReference)
        {
            ClearScreen();

            Console.WriteLine("*****************************************");
            Console.WriteLine("Booking Confirmed!");
            Console.WriteLine($"Booking Reference: {bookingReference}");
            Console.WriteLine("*****************************************");
        }

        public void DisplayAvailableFlights(IEnumerable<IFlight> flights)
        {
            FlightPresenter.DisplayFlights(flights);
        }

        public void Reset()
        {
            _isFormValid = true;

            _airlineCode = string.Empty;
            _flightNumber = -1;
            _flightDate = DateTime.Now;

            _firstName = string.Empty;
            _lastName = string.Empty;
            _birthDate = DateTime.Now;

            _paxCount = MAX_PAX_COUNT;
            _passengers = new List<PassengerEventArgs>();
        }

        public override void ShowContent()
        {
            searchReservationFlights();

            if (!_isFormValid)
            {
                return;
            }

            getBookingPassengers();
            displaySummary();
            submitForm();
        }

        private void searchReservationFlights()
        {
            Console.WriteLine();
            Console.WriteLine("-------Search for a Flight-------");

            getFlightDate();
            getAirlineCode();
            getFlightNumber();

            OnFlightSearched();
        }

        private void getFlightDate()
        {
            do
            {
                Console.Write($"Flight Date [{DATE_FORMAT}]: ");
                string? input = Console.ReadLine()?.Trim();
                _isFormValid = DateTime.TryParseExact(
                    input,
                    DATE_FORMAT,
                    _dateCulture,
                    DateTimeStyles.None,
                    out DateTime flightDate
                );

                if (!_isFormValid)
                {
                    SetFieldError(
                        nameof(FlightDate),
                        $"Please enter a valid date with the format \"{DATE_FORMAT}\"..."
                    );
                    continue;
                }

                FlightDate = flightDate;
            } while (!_isFormValid);
        }

        private void getAirlineCode()
        {
            do
            {
                Console.Write("Airline Code: ");
                string? input = Console.ReadLine() ?? string.Empty;

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

        private void getBookingPassengers()
        {
            while (_paxCount > 0)
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"***Enter details of passenger [{MAX_PAX_COUNT - _paxCount + 1}/{MAX_PAX_COUNT}]***"
                );

                getFirstName();
                getLastName();
                getBirthDate();

                OnAddPassenger();

                if (_paxCount > 1)
                {
                    char option = getYesOrNoInput("Add another passenger");
                    if (option == 'N')
                    {
                        _paxCount = 0;
                    }
                }
                _paxCount--;
            }
        }

        private void getFirstName()
        {
            do
            {
                Console.Write("First Name: ");
                string? input = Console.ReadLine()?.Trim();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError("FirstName", "Please enter a value...");
                    continue;
                }

                FirstName = input;
            } while (!_isFormValid);
        }

        private void getLastName()
        {
            do
            {
                Console.Write("Last Name: ");
                string? input = Console.ReadLine()?.Trim();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError("FirstName", "Please enter a value...");
                    continue;
                }

                LastName = input;
            } while (!_isFormValid);
        }

        private void getBirthDate()
        {
            do
            {
                Console.Write($"Birth Date [{DATE_FORMAT}]: ");
                string? input = Console.ReadLine();
                _isFormValid = DateTime.TryParseExact(
                    input,
                    DATE_FORMAT,
                    _dateCulture,
                    DateTimeStyles.None,
                    out DateTime birthDate
                );

                if (!_isFormValid)
                {
                    SetFieldError(
                        "BirthDate",
                        $"Please enter a valid date with the format \"{DATE_FORMAT}\"..."
                    );
                    continue;
                }

                BirthDate = birthDate;
            } while (!_isFormValid);
        }

        private void displaySummary()
        {
            ClearScreen();

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Booking Summary");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Flight Date: {FlightDate.ToShortDateString()}");
            Console.WriteLine();

            string flightDesignator =
                _selectedFlight.AirlineCode + " " + _selectedFlight.FlightNumber;
            string originDestination =
                _selectedFlight.DepartureStation + " -> " + _selectedFlight.ArrivalStation;
            string flightTime =
                _selectedFlight.DepartureScheduledTime.ToString("HH:mm")
                + "-"
                + _selectedFlight.ArrivalScheduledTime.ToString("HH:mm");

            Console.WriteLine($"Selected Flight Details_______");
            Console.WriteLine($"{flightDesignator} {originDestination} {flightTime}");
            Console.WriteLine();

            int count = 1;
            foreach (var passenger in _passengers)
            {
                Console.WriteLine($"Passenger #{count}_________________");
                Console.WriteLine($"Name: {passenger.FirstName} {passenger.LastName}");
                Console.WriteLine($"Birth Date: {passenger.BirthDate.ToShortDateString()}");
                Console.WriteLine();

                count++;
            }

            Console.WriteLine("-------------------------------------------");
        }

        private void submitForm()
        {
            char option = getYesOrNoInput("Create reservation");

            if (option == 'Y')
            {
                OnSubmitted();
            }
        }

        private void OnFlightDateChanged()
        {
            FlightDateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnAirlineCodeChanged()
        {
            AirlineCodeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnFlightNumberChanged()
        {
            FlightNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnFirstNameChanged()
        {
            FirstNameChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnLastNameChanged()
        {
            LastNameChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnBirthDateChanged()
        {
            BirthDateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnAddPassenger()
        {
            var args = new PassengerEventArgs()
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate
            };

            _passengers.Add(args);
        }

        private void OnSubmitted()
        {
            var args = new ReservationEventArgs()
            {
                FlightDate = FlightDate,
                FlightInfo = _selectedFlight,
                Passengers = _passengers
            };

            Submitted?.Invoke(this, args);
        }

        private void OnFlightSearched()
        {
            FlightSearched?.Invoke(this, EventArgs.Empty);
        }

        public void SetFieldError(string paramName, string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void DisplayNoFlights()
        {
            string flightDesignator = AirlineCode + " " + FlightNumber;
            AlertError(
                header: "No Available Flights",
                message: $"No ({flightDesignator}) flights scheduled on {FlightDate.ToShortDateString()}"
            );
        }

        public void ShowFlightSelection(IEnumerable<IFlight> flights)
        {
            IFlight? selectedFlight = null;
            do
            {
                Console.Write("Choose a Flight: ");
                string? input = Console.ReadLine();
                _isFormValid = int.TryParse(input, out int flightIndex);
                if (!_isFormValid)
                {
                    Console.WriteLine("Please enter a numeric value...");
                    Console.WriteLine();
                    continue;
                }

                flightIndex -= 1;
                _isFormValid = flightIndex >= 0 && flightIndex < flights.Count();
                if (!_isFormValid)
                {
                    Console.WriteLine("Please enter a valid option...");
                    Console.WriteLine();
                    continue;
                }

                selectedFlight = flights.ElementAt(flightIndex);
            } while (!_isFormValid);

            _selectedFlight = new FlightEventArgs()
            {
                AirlineCode = selectedFlight.AirlineCode,
                FlightNumber = selectedFlight.FlightNumber,
                DepartureStation = selectedFlight.DepartureStation,
                ArrivalStation = selectedFlight.ArrivalStation,
                DepartureScheduledTime = selectedFlight.DepartureScheduledTime,
                ArrivalScheduledTime = selectedFlight.ArrivalScheduledTime,
            };
        }
    }
}
