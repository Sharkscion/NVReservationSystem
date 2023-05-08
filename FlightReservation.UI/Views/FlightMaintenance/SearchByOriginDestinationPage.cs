using FlightReservation.Models.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByOriginDestinationPage : BasePage, ISearchByOriginDestinationView
    {
        private bool _isFormValid;
        private string _departureStation;
        private string _arrivalStation;

        public string DepartureStation
        {
            get { return _departureStation; }
            set
            {
                _departureStation = value;
                OnDepartureStationCodeChanged();
            }
        }
        public string ArrivalStation
        {
            get { return _arrivalStation; }
            set
            {
                _arrivalStation = value;
                OnArrivalStationCodeChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

        public event EventHandler DepartureStationChanged;
        public event EventHandler ArrivalStationChanged;
        public event EventHandler Submitted;

        public SearchByOriginDestinationPage(string title)
            : base(title)
        {
            Reset();
        }

        public void Display(IEnumerable<IFlight> flights)
        {
            ClearScreen();

            Console.WriteLine("\n-----------------------------------------------");
            FlightPresenter.DisplayFlights(flights);
            Console.WriteLine("-----------------------------------------------\n");
        }

        public void Reset()
        {
            _isFormValid = true;
            _departureStation = string.Empty;
            _arrivalStation = string.Empty;
        }

        public override void ShowContent()
        {
            getDepartureStationCode();
            getArrivalStationCode();
            OnSubmitted();
        }

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

        private void OnDepartureStationCodeChanged()
        {
            DepartureStationChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnArrivalStationCodeChanged()
        {
            ArrivalStationChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }

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

        public void DisplayNoFlights()
        {
            ClearScreen();

            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine($"No flights going from {DepartureStation} to {ArrivalStation}...");
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
