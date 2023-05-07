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

        private bool _isFormValid;
        private int _paxCount;

        private CultureInfo _dateCulture;

        private List<PassengerEventArgs> _passengers;
        private FlightEventArgs _selectedFlight;

        public DateTime FlightDate { get; set; }
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

        public CreateReservationPage(string title)
            : base(title)
        {
            _dateCulture = new CultureInfo("en-US");
            Reset();
        }

        public event IFormView.InputChangedEventHandler<DateTime> FlightDateChanged;
        public event IFormView.InputChangedEventHandler<string> AirlineCodeChanged;
        public event IFormView.InputChangedEventHandler<int> FlightNumberChanged;
        public event IFormView.InputChangedEventHandler<string> FirstNameChanged;
        public event IFormView.InputChangedEventHandler<string> LastNameChanged;
        public event IFormView.InputChangedEventHandler<DateTime> BirthDateChanged;

        public event IFormView.SubmitEventHandler<SearchAvailableFlightEventArgs> FlightSearched;
        public event IFormView.SubmitEventHandler<ReservationEventArgs> Submitted;

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
            if (flights.Count() == 0)
            {
                string flightDesignator = AirlineCode + " " + FlightNumber;
                AlertError(
                    header: "No Available Flights",
                    message: $"No ({flightDesignator}) flights scheduled on {FlightDate.ToShortDateString()}"
                );
                return;
            }

            FlightPresenter.DisplayFlights(flights);
            _selectedFlight = selectAFlight(flights);
        }

        public void Reset()
        {
            _isFormValid = true;

            AirlineCode = string.Empty;
            FlightNumber = -1;
            FlightDate = DateTime.Now;

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

        private DateTime getFlightDate()
        {
            DateTime flightDate;
            do
            {
                Console.Write($"Flight Date [{DATE_FORMAT}]: ");
                string? input = Console.ReadLine()?.Trim();
                _isFormValid = DateTime.TryParseExact(
                    input,
                    DATE_FORMAT,
                    _dateCulture,
                    DateTimeStyles.None,
                    out flightDate
                );

                if (!_isFormValid)
                {
                    SetFieldError(
                        nameof(FlightDate),
                        $"Please enter a valid date with the format \"{DATE_FORMAT}\"..."
                    );
                    continue;
                }

                OnFlightDateChanged(flightDate);
            } while (!_isFormValid);

            return flightDate;
        }

        private string getAirlineCode()
        {
            string airlineCode;

            do
            {
                Console.Write("Airline Code: ");
                airlineCode = Console.ReadLine() ?? string.Empty;

                _isFormValid = airlineCode is not null && airlineCode != string.Empty;
                if (!_isFormValid)
                {
                    SetFieldError(nameof(AirlineCode), "Please enter a value...");
                    continue;
                }

                OnAirlineCodeChanged(airlineCode);
            } while (!_isFormValid);

            return airlineCode;
        }

        private int getFlightNumber()
        {
            int flightNumber;

            do
            {
                Console.Write("Flight Number: ");
                string? input = Console.ReadLine();

                _isFormValid = int.TryParse(input, out flightNumber);
                if (!_isFormValid)
                {
                    SetFieldError(nameof(FlightNumber), "Please enter a numeric value...");
                    continue;
                }

                OnFlightNumberChanged(flightNumber);
            } while (!_isFormValid);

            return flightNumber;
        }

        private void getBookingPassengers()
        {
            while (_paxCount > 0)
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"***Enter details of passenger [{MAX_PAX_COUNT - _paxCount + 1}/{MAX_PAX_COUNT}]***"
                );

                string firstName = getFirstName();
                string lastName = getLastName();
                DateTime birthDate = getBirthDate();

                OnAddPassenger(firstName, lastName, birthDate);

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

        private string getFirstName()
        {
            string firstName;

            do
            {
                Console.Write("First Name: ");
                firstName = Console.ReadLine()?.Trim() ?? string.Empty;
                _isFormValid = firstName is not null && firstName != string.Empty;

                if (!_isFormValid)
                {
                    SetFieldError("FirstName", "Please enter a value...");
                    continue;
                }

                OnFirstNameChanged(firstName);
            } while (!_isFormValid);

            return firstName;
        }

        private string getLastName()
        {
            string lastName;

            do
            {
                Console.Write("Last Name: ");
                lastName = Console.ReadLine()?.Trim() ?? string.Empty;
                _isFormValid = lastName is not null && lastName != string.Empty;

                if (!_isFormValid)
                {
                    SetFieldError("FirstName", "Please enter a value...");
                    continue;
                }

                OnLastNameChanged(lastName);
            } while (!_isFormValid);

            return lastName;
        }

        private DateTime getBirthDate()
        {
            DateTime birthDate;

            do
            {
                Console.Write($"Birth Date [{DATE_FORMAT}]: ");
                string? input = Console.ReadLine();
                _isFormValid = DateTime.TryParseExact(
                    input,
                    DATE_FORMAT,
                    _dateCulture,
                    DateTimeStyles.None,
                    out birthDate
                );

                if (!_isFormValid)
                {
                    SetFieldError(
                        "BirthDate",
                        $"Please enter a valid date with the format \"{DATE_FORMAT}\"..."
                    );
                    continue;
                }

                OnBirthDateChanged(birthDate);
            } while (!_isFormValid);

            return birthDate;
        }

        private FlightEventArgs selectAFlight(IEnumerable<IFlight> flight)
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
                _isFormValid = flightIndex >= 0 && flightIndex < flight.Count();
                if (!_isFormValid)
                {
                    Console.WriteLine("Please enter a valid option...");
                    Console.WriteLine();
                    continue;
                }

                selectedFlight = flight.ElementAt(flightIndex);
            } while (!_isFormValid);

            return new FlightEventArgs()
            {
                AirlineCode = selectedFlight.AirlineCode,
                FlightNumber = selectedFlight.FlightNumber,
                DepartureStation = selectedFlight.DepartureStation,
                ArrivalStation = selectedFlight.ArrivalStation,
                DepartureScheduledTime = selectedFlight.DepartureScheduledTime,
                ArrivalScheduledTime = selectedFlight.ArrivalScheduledTime,
            };
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

        private void OnFlightDateChanged(DateTime value)
        {
            FlightDate = value;
            FlightDateChanged?.Invoke(this, new ChangeEventArgs<DateTime>(value));
        }

        private void OnAirlineCodeChanged(string value)
        {
            AirlineCode = value;
            AirlineCodeChanged?.Invoke(this, new ChangeEventArgs<string>(value));
        }

        private void OnFlightNumberChanged(int value)
        {
            FlightNumber = value;
            FlightNumberChanged?.Invoke(this, new ChangeEventArgs<int>(value));
        }

        private void OnFirstNameChanged(string value)
        {
            FirstNameChanged?.Invoke(this, new ChangeEventArgs<string>(value));
        }

        private void OnLastNameChanged(string value)
        {
            LastNameChanged?.Invoke(this, new ChangeEventArgs<string>(value));
        }

        private void OnBirthDateChanged(DateTime value)
        {
            BirthDateChanged?.Invoke(this, new ChangeEventArgs<DateTime>(value));
        }

        private void OnAddPassenger(string firstName, string lastName, DateTime birthDate)
        {
            var args = new PassengerEventArgs()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate
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
            var args = new SearchAvailableFlightEventArgs()
            {
                FlightDate = FlightDate,
                AirlineCode = AirlineCode,
                FlightNumber = FlightNumber
            };

            FlightSearched?.Invoke(this, args);
        }

        public void SetFieldError(string paramName, string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
