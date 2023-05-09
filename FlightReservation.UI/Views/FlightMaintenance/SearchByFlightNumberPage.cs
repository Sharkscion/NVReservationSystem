using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByFlightNumberPage : BasePage, ISearchByFlightNumberView
    {
        #region Declarations
        private bool _isFormValid;
        private int _flightNumber;
        #endregion

        #region Properties
        public int FlightNumber
        {
            get { return _flightNumber; }
            set
            {
                _flightNumber = value;
                onFlightNumberChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }
        #endregion

        #region Constructors
        public SearchByFlightNumberPage(string title)
            : base(title)
        {
            Reset();
        }
        #endregion

        #region Events
        public event EventHandler FlightNumberChanged;
        public event EventHandler Submitted;
        #endregion

        #region Overridden Methods
        public override void ShowContent()
        {
            getFlightNumber();
            onSubmitted();
        }
        #endregion

        #region Implementations of ISearchByFlightNumber
        public void Display(IEnumerable<IFlight> flights)
        {
            ClearScreen();

            Console.WriteLine("\n-----------------------------------------------");
            FlightPresenter.DisplayFlights(flights);
            Console.WriteLine("-----------------------------------------------\n");
        }

        public void DisplayNoFlights()
        {
            ClearScreen();
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine($"No flights found with flight number ({FlightNumber})...");
            Console.WriteLine("-----------------------------------------------");
        }
        #endregion

        #region Implementations of IFormView
        public void Reset()
        {
            _isFormValid = true;
            _flightNumber = -1;
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

        #region Event Invocation Methods
        private void onFlightNumberChanged()
        {
            FlightNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
