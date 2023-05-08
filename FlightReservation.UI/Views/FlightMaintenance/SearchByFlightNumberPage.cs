using FlightReservation.Models.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByFlightNumberPage : BasePage, ISearchByFlightNumberView
    {
        private bool _isFormValid;
        private int _flightNumber;

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

        public SearchByFlightNumberPage(string title)
            : base(title)
        {
            Reset();
        }

        public event EventHandler FlightNumberChanged;
        public event EventHandler Submitted;

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
            _flightNumber = -1;
        }

        public override void ShowContent()
        {
            getFlightNumber();
            OnSubmitted();
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
                    SetFieldError(nameof(FlightNumber), "Please input a numeric value...");
                    continue;
                }

                FlightNumber = flightNumber;
            } while (!_isFormValid);
        }

        private void OnFlightNumberChanged()
        {
            FlightNumberChanged?.Invoke(this, EventArgs.Empty);
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
            Console.WriteLine($"No flights found with flight number ({FlightNumber})...");
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
