using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByAirlineCodePage : BasePage, ISearchByAirlineCodeView
    {
        #region Declarations
        private bool _isFormValid;
        private string _airlineCode;
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

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }
        #endregion

        #region Constructors
        public SearchByAirlineCodePage(string title)
            : base(title)
        {
            Reset();
        }
        #endregion

        #region Events
        public event EventHandler AirlineCodeChanged;
        public event EventHandler Submitted;
        #endregion


        #region Overridden Methods
        public override void ShowContent()
        {
            getAirlineCode();
            onSubmitted();
        }
        #endregion

        #region Implementations of IFormView
        public void Reset()
        {
            _airlineCode = string.Empty;
            _isFormValid = true;
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
        #endregion

        #region Implementations of ISearchByAirlineCodeView
        public void Display(IEnumerable<IFlight> flights)
        {
            ClearScreen();

            Console.WriteLine();
            Console.WriteLine($"    Flights with airline code ({AirlineCode})");

            FlightPresenter.DisplayFlights(flights);
        }

        public void DisplayNoFlights()
        {
            ClearScreen();

            Console.WriteLine("\n--------------------------------------------------------------");
            Console.WriteLine($"No flights found with airline code ({AirlineCode})...");
            Console.WriteLine("--------------------------------------------------------------");
        }
        #endregion

        #region Private Methods
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
        #endregion

        #region Event Invocation Methods
        private void onAirlineCodeChanged()
        {
            AirlineCodeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
