﻿using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Views.Reservation
{
    internal class SearchReservationPage : BasePage, ISearchReservationView
    {
        #region Declarations
        private bool _isFormValid;
        private string _PNR;
        #endregion

        #region Properties
        public string PNR
        {
            get { return _PNR; }
            set
            {
                _PNR = value;
                onPNRChanged();
            }
        }

        public bool IsFormValid
        {
            get { return _isFormValid; }
        }
        #endregion

        #region Events
        public event EventHandler PNRChanged;
        public event EventHandler Submitted;
        #endregion

        #region Constructors
        public SearchReservationPage(string title)
            : base(title)
        {
            Reset();
        }
        #endregion

        #region Overridden Methods
        public override void ShowContent()
        {
            getPNR();
            onSubmitted();
        }
        #endregion

        #region Implementations of ISearchReservationView
        public void DisplayNoReservation()
        {
            ClearScreen();

            Console.WriteLine("\n------------------------------------------------------------");
            Console.WriteLine($" No reservation found with a booking reference '{PNR}'...");
            Console.WriteLine("--------------------------------------------------------------");
        }

        public void DisplayReservation(IReservation reservation)
        {
            ClearScreen();

            Console.WriteLine(
                "---------------------------------------------------------------------"
            );
            Console.WriteLine($"Reservation Details of Booking Reference ({PNR})");
            Console.WriteLine(
                "---------------------------------------------------------------------"
            );

            Console.WriteLine($"Flight Date: {reservation.FlightDate.ToShortDateString()}");
            Console.WriteLine();

            string flightDesignator =
                reservation.FlightInfo.AirlineCode + " " + reservation.FlightInfo.FlightNumber;
            string originDestination =
                reservation.FlightInfo.DepartureStation
                + " -> "
                + reservation.FlightInfo.ArrivalStation;
            string flightTime =
                reservation.FlightInfo.DepartureScheduledTimeString
                + "-"
                + reservation.FlightInfo.ArrivalScheduledTimeString;

            Console.WriteLine($"[Booked Flight Details]");
            Console.WriteLine($"Flight Designator: {flightDesignator}");
            Console.WriteLine($"From/To: {originDestination}");
            Console.WriteLine($"Flight Time: {flightTime}");
            Console.WriteLine();

            int count = 1;
            foreach (var passenger in reservation.Passengers)
            {
                Console.WriteLine($"[Passenger #{count}]");
                Console.WriteLine($"Name: {passenger.FirstName} {passenger.LastName}");
                Console.WriteLine($"Birth Date: {passenger.BirthDate.ToShortDateString()}");
                Console.WriteLine($"Age: {passenger.Age}");
                Console.WriteLine();

                count++;
            }

            Console.WriteLine(
                "---------------------------------------------------------------------"
            );
        }
        #endregion

        #region Implementations of IFormView
        public void Reset()
        {
            _isFormValid = true;
            _PNR = string.Empty;
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

        #region Private Methods
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
        #endregion

        #region Event Invocation Methods
        private void onPNRChanged()
        {
            PNRChanged?.Invoke(this, EventArgs.Empty);
        }

        private void onSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
