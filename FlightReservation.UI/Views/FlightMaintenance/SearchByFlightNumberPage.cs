using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByFlightNumberPage : BasePage, ISearchByFlightNumberView
    {
        private bool _isFormValid;

        public int FlightNumber { get; set; }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

        public SearchByFlightNumberPage(string title)
            : base(title)
        {
            Reset();
        }

        public event IFormView.InputChangedEventHandler<int> FlightNumberChanged;
        public event IFormView.SubmitEventHandler<SubmitEventArgs<int>> Submitted;

        public void Display(IEnumerable<IFlight> flights)
        {
            ClearScreen();

            Console.WriteLine("\n-----------------------------------------------");

            if (flights.Count() == 0)
            {
                Console.WriteLine($"No flights found with flight number ({FlightNumber})...");
            }
            else
            {
                FlightPresenter.DisplayFlights(flights);
            }

            Console.WriteLine("-----------------------------------------------\n");
        }

        public void Reset()
        {
            _isFormValid = true;
            FlightNumber = -1;
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

                OnFlightNumberChanged(flightNumber);
            } while (!_isFormValid);
        }

        private void OnFlightNumberChanged(int flightNumber)
        {
            FlightNumber = flightNumber;
            FlightNumberChanged?.Invoke(this, new ChangeEventArgs<int>(FlightNumber));
        }

        private void OnSubmitted()
        {
            var args = new SubmitEventArgs<int>(FlightNumber);
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
