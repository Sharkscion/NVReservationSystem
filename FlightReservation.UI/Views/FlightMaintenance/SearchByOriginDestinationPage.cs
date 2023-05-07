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

        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

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
                Console.WriteLine(
                    $"No flights going from {DepartureStation} to {ArrivalStation}..."
                );
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
            DepartureStation = string.Empty;
            ArrivalStation = string.Empty;
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
                    SetFieldError(nameof(DepartureStation), "Please enter a value");
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
                    SetFieldError(nameof(ArrivalStation), "Please enter a value");
                    continue;
                }

                OnArrivalStationCodeChanged(input);
            } while (!_isFormValid);
        }

        private void OnDepartureStationCodeChanged(string value)
        {
            DepartureStation = value;
            DepartureStationChanged?.Invoke(this, new ChangeEventArgs<string>(DepartureStation));
        }

        private void OnArrivalStationCodeChanged(string value)
        {
            ArrivalStation = value;
            ArrivalStationChanged?.Invoke(this, new ChangeEventArgs<string>(ArrivalStation));
        }

        private void OnSubmitted()
        {
            var data = new Tuple<string, string>(DepartureStation, ArrivalStation);
            var args = new SubmitEventArgs<Tuple<string, string>>(data);

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
