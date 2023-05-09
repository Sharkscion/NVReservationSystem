using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByOriginDestinationPage : BasePage, ISearchByOriginDestinationView
    {
        #region Declarations
        private bool _isFormValid;
        private string _departureStation;
        private string _arrivalStation;
        #endregion

        #region Properties
        public string DepartureStation
        {
            get { return _departureStation; }
            set
            {
                _departureStation = value;
                onDepartureStationCodeChanged();
            }
        }
        public string ArrivalStation
        {
            get { return _arrivalStation; }
            set
            {
                _arrivalStation = value;
                onArrivalStationCodeChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }
        #endregion

        #region Events
        public event EventHandler DepartureStationChanged;
        public event EventHandler ArrivalStationChanged;
        public event EventHandler Submitted;
        #endregion

        #region Constructors
        public SearchByOriginDestinationPage(string title)
            : base(title)
        {
            Reset();
        }
        #endregion

        #region Overridden Methods
        public override void ShowContent()
        {
            getDepartureStationCode();
            getArrivalStationCode();
            onSubmitted();
        }
        #endregion

        #region Implementations of ISearchByOriginDestinationView
        public void Display(IEnumerable<IFlight> flights)
        {
            ClearScreen();

            Console.WriteLine();
            Console.WriteLine($"    Flights from {DepartureStation} to {ArrivalStation}");

            FlightPresenter.DisplayFlights(flights);
        }

        public void DisplayNoFlights()
        {
            ClearScreen();

            Console.WriteLine("\n--------------------------------------------------------------");
            Console.WriteLine($"No flights going from {DepartureStation} to {ArrivalStation}...");
            Console.WriteLine("--------------------------------------------------------------");
        }
        #endregion


        #region Implementations of IFormView
        public void SetFieldError(string paramName, string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
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

        public void Reset()
        {
            _isFormValid = true;
            _departureStation = string.Empty;
            _arrivalStation = string.Empty;
        }
        #endregion

        #region Private Methods
        private void getDepartureStationCode()
        {
            do
            {
                Console.Write("Departure Station Code: ");
                string? input = Console.ReadLine()?.Trim();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError(nameof(DepartureStation), "Please enter a value");
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
                string? input = Console.ReadLine()?.Trim();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError(nameof(ArrivalStation), "Please enter a value");
                    continue;
                }

                ArrivalStation = input;
            } while (!_isFormValid);
        }
        #endregion

        #region Event Invocation Methods
        private void onDepartureStationCodeChanged()
        {
            DepartureStationChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onArrivalStationCodeChanged()
        {
            ArrivalStationChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
