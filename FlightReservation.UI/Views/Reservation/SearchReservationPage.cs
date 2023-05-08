using FlightReservation.Models.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Views.Reservation
{
    internal class SearchReservationPage : BasePage, ISearchReservationView
    {
        private bool _isFormValid;
        private string _PNR;

        public string PNR
        {
            get { return _PNR; }
            set
            {
                _PNR = value;
                OnPNRChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

        public SearchReservationPage(string title)
            : base(title)
        {
            Reset();
        }

        public event EventHandler PNRChanged;
        public event EventHandler Submitted;

        public void DisplayReservation(IReservation reservation)
        {
            ClearScreen();

            Console.WriteLine("\n-------------------------------------------");

            Console.WriteLine($"Flight Date: {reservation.FlightDate.ToShortDateString()}");
            Console.WriteLine();

            string flightDesignator =
                reservation.FlightInfo.AirlineCode + " " + reservation.FlightInfo.FlightNumber;
            string originDestination =
                reservation.FlightInfo.DepartureStation
                + " -> "
                + reservation.FlightInfo.ArrivalStation;
            string flightTime =
                reservation.FlightInfo.DepartureScheduledTime.ToString("HH:mm")
                + "-"
                + reservation.FlightInfo.ArrivalScheduledTime.ToString("HH:mm");

            Console.WriteLine($"Booked Flight Details_______");
            Console.WriteLine($"{flightDesignator} {originDestination} {flightTime}");
            Console.WriteLine();

            int count = 1;
            foreach (var passenger in reservation.Passengers)
            {
                Console.WriteLine($"Passenger #{count}_________________");
                Console.WriteLine($"Name: {passenger.FirstName} {passenger.LastName}");
                Console.WriteLine($"Birth Date: {passenger.BirthDate.ToShortDateString()}");
                Console.WriteLine($"Age: {passenger.Age}");
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------------");
        }

        public void Reset()
        {
            _isFormValid = true;
            _PNR = string.Empty;
        }

        public override void ShowContent()
        {
            getPNR();
            OnSubmitted();
        }

        private void getPNR()
        {
            do
            {
                Console.Write("Booking Reference: ");
                string? input = Console.ReadLine()?.Trim();

                _isFormValid = !string.IsNullOrEmpty(input);
                if (!_isFormValid)
                {
                    SetFieldError(nameof(PNR), "Please enter a value...");
                    continue;
                }

                PNR = input;
            } while (!_isFormValid);
        }

        private void OnPNRChanged()
        {
            PNRChanged?.Invoke(this, EventArgs.Empty);
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

        public void DisplayNoReservation()
        {
            ClearScreen();

            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine($"No reservation found with a booking reference '{PNR}'...");
            Console.WriteLine("-------------------------------------------");
        }
    }
}
