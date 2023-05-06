using FlightReservation.Data.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByFlightNumberPage : BasePage, ISearchByFlightNumberView
    {
        private bool _isFormValid;
        private int _flightNumber;

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
                Console.WriteLine($"No flights found with flight number ({_flightNumber})...");
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
            _flightNumber = -1;
        }

        public void SetFlightNumberError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
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
                    SetFlightNumberError("Please input a numeric value...");
                    continue;
                }

                OnFlightNumberChanged(flightNumber);
            } while (!_isFormValid);
        }

        private void OnFlightNumberChanged(int flightNumber)
        {
            _flightNumber = flightNumber;
            FlightNumberChanged?.Invoke(this, new ChangeEventArgs<int>(_flightNumber));
        }

        private void OnSubmitted()
        {
            var args = new SubmitEventArgs<int>(_flightNumber);
            Submitted?.Invoke(this, args);
        }
    }
}
