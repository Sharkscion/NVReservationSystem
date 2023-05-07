using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;
using System.Text;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByAirlineCodePage : BasePage, ISearchByAirlineCodeView
    {
        private string _airlineCode;
        private bool _isFormValid;

        public event IFormView.InputChangedEventHandler<string> AirlineCodeChanged;
        public event IFormView.SubmitEventHandler<SubmitEventArgs<string>> Submitted;

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
                Console.WriteLine($"No flights found with airline code ({_airlineCode})...");
            }
            else
            {
                FlightPresenter.DisplayFlights(flights);
            }

            Console.WriteLine("-----------------------------------------------\n");
        }

        public void Reset()
        {
            _airlineCode = string.Empty;
            _isFormValid = true;
        }

        public void SetAirlineCodeError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
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
            _airlineCode = airlineCode;
            AirlineCodeChanged?.Invoke(this, new ChangeEventArgs<string>(_airlineCode));
        }

        private void OnSubmitted()
        {
            var args = new SubmitEventArgs<string>(_airlineCode);
            Submitted?.Invoke(this, args);
        }
    }
}
