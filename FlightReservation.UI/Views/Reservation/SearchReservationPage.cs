using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Views.Reservation
{
    internal class SearchReservationPage : BasePage, ISearchReservationView
    {
        private bool _isFormValid;

        public string PNR { get; set; }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }

        public SearchReservationPage(string title)
            : base(title)
        {
            Reset();
        }

        public event IFormView.InputChangedEventHandler<string> PNRChanged;
        public event IFormView.SubmitEventHandler<SubmitEventArgs<string>> Submitted;

        public void DisplayReservation(IReservation? reservation)
        {
            ClearScreen();

            Console.WriteLine("-------------------------------------------");

            if (reservation == null)
            {
                displayNoReservationFound();
            }
            else
            {
                displaySearchedReservation(reservation);
            }

            Console.WriteLine("-------------------------------------------");
        }

        public void Reset()
        {
            _isFormValid = true;
            PNR = string.Empty;
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

                _isFormValid = input is not null && input != string.Empty;
                if (!_isFormValid)
                {
                    SetFieldError(nameof(PNR), "Please enter a value...");
                    continue;
                }

                OnPNRChanged(input);
            } while (!_isFormValid);
        }

        private void displayNoReservationFound()
        {
            Console.WriteLine($"No reservation found with a booking reference '{PNR}'...");
        }

        private void displaySearchedReservation(IReservation reservation)
        {
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
        }

        private void OnPNRChanged(string value)
        {
            PNR = value;
            PNRChanged?.Invoke(this, new ChangeEventArgs<string>(value));
        }

        private void OnSubmitted()
        {
            Submitted?.Invoke(this, new SubmitEventArgs<string>(PNR));
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
