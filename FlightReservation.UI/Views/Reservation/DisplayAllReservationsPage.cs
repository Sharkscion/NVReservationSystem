﻿using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Views.Reservation
{
    internal class DisplayAllReservationsPage : BasePage, IDisplayAllReservationsView
    {
        #region Events
        public event EventHandler Submitted;

        #endregion

        #region Constructors
        public DisplayAllReservationsPage(string title)
            : base(title) { }
        #endregion

        #region Overridden Methods
        public override void ShowContent()
        {
            onSubmitted();
        }
        #endregion

        #region Implementations of IDisplayAllReservationsView
        public void DisplayReservations(IEnumerable<IReservation> reservations)
        {
            ClearScreen();

            Console.WriteLine(
                "\n-------------------------------------------------------------------------------"
            );

            var header = String.Format(
                "{0,3} {1,-6} {2,5} {3,-15} {4,-18} {5, -10} {6,-15}",
                "#",
                "PNR",
                "Pax",
                "Flight Date",
                "Flight Designator",
                "From->To",
                "Flight Time"
            );
            Console.WriteLine(header);
            Console.WriteLine(
                "-------------------------------------------------------------------------------"
            );

            int count = 1;
            foreach (var reservation in reservations)
            {
                var flightDesignator =
                    reservation.FlightInfo.AirlineCode + " " + reservation.FlightInfo.FlightNumber;

                var originDestination =
                    reservation.FlightInfo.DepartureStation
                    + "->"
                    + reservation.FlightInfo.ArrivalStation;

                var flightTime =
                    reservation.FlightInfo.DepartureScheduledTimeString
                    + " - "
                    + reservation.FlightInfo.ArrivalScheduledTimeString;

                var output = String.Format(
                    "{0,3} {1,-6} {2,5} {3,-15} {4,-18} {5, -10} {6,-15}\n",
                    count,
                    reservation.PNR,
                    reservation.Passengers.Count(),
                    reservation.FlightDate.ToShortDateString(),
                    flightDesignator,
                    originDestination,
                    flightTime
                );

                Console.WriteLine(output);
                count++;
            }
            Console.WriteLine(
                "-------------------------------------------------------------------------------"
            );
        }

        public void DisplayNoReservations()
        {
            ClearScreen();
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(" No available reservations to be displayed.");
            Console.WriteLine("-----------------------------------------------");
        }
        #endregion

        #region Event Invocation Methods
        private void onSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
