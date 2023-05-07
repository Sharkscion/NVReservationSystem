using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Utilities;

namespace FlightReservation.UI.Views.FlightMaintenance
{
    internal class SearchByOriginDestinationPage : BasePage, ISearchByOriginDestinationView
    {
        private bool _isFormValid;
        private string _origin;
        private string _destination;

        public event IFormView.InputChangedEventHandler<string> DepartureStationChanged;
        public event IFormView.InputChangedEventHandler<string> ArrivalStationChanged;
        public event IFormView.SubmitEventHandler<SubmitEventArgs<Tuple<string, string>>> Submitted;

        public SearchByOriginDestinationPage(string title)
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
                Console.WriteLine($"No flights going from {_origin} to {_destination}...");
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
            _origin = string.Empty;
            _destination = string.Empty;
        }

        public void SetArrivalStationError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void SetDepartureStationError(string message)
        {
            _isFormValid = false;
            Console.WriteLine(message);
            Console.WriteLine();
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
                _isFormValid = input is not null && input != string.Empty;

                if (!_isFormValid)
                {
                    Console.WriteLine("Please enter a value...");
                    continue;
                }

                OnDepartureStationCodeChanged(input);
            } while (!_isFormValid);
        }

        private void getArrivalStationCode()
        {
            do
            {
                Console.Write("Arrival Station Code: ");
                string? input = Console.ReadLine()?.Trim();
                _isFormValid = input is not null && input != string.Empty;

                if (!_isFormValid)
                {
                    Console.WriteLine("Please enter a value...");
                    continue;
                }

                OnArrivalStationCodeChanged(input);
            } while (!_isFormValid);
        }

        private void OnDepartureStationCodeChanged(string value)
        {
            _origin = value;
            DepartureStationChanged?.Invoke(this, new ChangeEventArgs<string>(_origin));
        }

        private void OnArrivalStationCodeChanged(string value)
        {
            _destination = value;
            ArrivalStationChanged?.Invoke(this, new ChangeEventArgs<string>(_destination));
        }

        private void OnSubmitted()
        {
            var data = new Tuple<string, string>(_origin, _destination);
            var args = new SubmitEventArgs<Tuple<string, string>>(data);

            Submitted?.Invoke(this, args);
        }
    }
}
