using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByAirlineCodePage : BasePage, ISearchByAirlineCodeView
    {
        private bool _isFormValid;

        public event IFormView.InputChangedEventHandler<string> AirlineCodeChanged;
        public event IFormView.SubmitEventHandler<SubmitEventArgs<string>> Submitted;

        public string AirlineCode { get; set; }
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

            if (flights.Count() == 0)
            {
                Console.WriteLine($"No flights found with airline code ({AirlineCode})...");
            }
            else
            {
                FlightPresenter.DisplayFlights(flights);
            }

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
                _isFormValid = input is not null && input != string.Empty;

                if (!_isFormValid)
                {
                    Console.WriteLine("Please enter a value...");
                    continue;
                }

                OnAirlineCodeChanged(input);
            } while (!_isFormValid);
        }

        private void OnAirlineCodeChanged(string airlineCode)
        {
            AirlineCode = airlineCode;
            AirlineCodeChanged?.Invoke(this, new ChangeEventArgs<string>(AirlineCode));
        }

        private void OnSubmitted()
        {
            var args = new SubmitEventArgs<string>(AirlineCode);
            Submitted?.Invoke(this, args);
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
    }
}
