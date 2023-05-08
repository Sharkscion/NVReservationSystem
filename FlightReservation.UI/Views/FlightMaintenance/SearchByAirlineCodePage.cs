using FlightReservation.Models.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByAirlineCodePage : BasePage, ISearchByAirlineCodeView
    {
        private bool _isFormValid;
        private string _airlineCode;

        public event EventHandler AirlineCodeChanged;
        public event EventHandler Submitted;

        public string AirlineCode
        {
            get { return _airlineCode; }
            set
            {
                _airlineCode = value;
                OnAirlineCodeChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

        public SearchByAirlineCodePage(string title)
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
            AirlineCode = string.Empty;
            _isFormValid = true;
        }

        public override void ShowContent()
        {
            getAirlineCode();
            OnSubmitted();
        }

        private void getAirlineCode()
        {
            do
            {
                Console.Write("Airline Code: ");
                string? input = Console.ReadLine()?.Trim();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    Console.WriteLine("Please enter a value...");
                    continue;
                }

                AirlineCode = input;
            } while (!_isFormValid);
        }

        private void OnAirlineCodeChanged()
        {
            AirlineCodeChanged?.Invoke(this, EventArgs.Empty);
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
            Console.WriteLine($"No flights found with airline code ({AirlineCode})...");
            Console.WriteLine("-----------------------------------------------\n");
        }
    }
}
