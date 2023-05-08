using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.Reservation.Contracts;

namespace FlightReservation.UI.Views.Reservation
{
    internal class DisplayAllReservationsPage : BasePage, IDisplayAllReservationsView
    {
        public DisplayAllReservationsPage(string title)
            : base(title) { }

        public event EventHandler Submitted;

        public override void ShowContent()
        {
            OnSubmitted();
        }

        public void OnSubmitted()
        {
            Submitted?.Invoke(this, EventArgs.Empty);
        }

        public void DisplayReservations(IEnumerable<IReservation> reservations)
        {
            ClearScreen();

            Console.WriteLine("\n-----------------------------------------------");

            var header = String.Format(
                "{0,3} {1,-6} {2,3} {3,-15} {4,-15} {5, -15} {6,-15}\n",
                "#",
                "PNR",
                "Pax",
                "Flight Date",
                "Flight Desinator",
                "From -> To",
                "Flight Time"
            );
            Console.WriteLine(header);

            int count = 1;
            foreach (var reservation in reservations)
            {
                var flightDesignator =
                    reservation.FlightInfo.AirlineCode + " " + reservation.FlightInfo.FlightNumber;

                var originDestination =
                    reservation.FlightInfo.DepartureStation
                    + " -> "
                    + reservation.FlightInfo.ArrivalStation;

                var flightTime =
                    reservation.FlightInfo.DepartureScheduledTime.ToString("HH:mm")
                    + " - "
                    + reservation.FlightInfo.ArrivalScheduledTime.ToString("HH:mm");

                var output = String.Format(
                    "{0,3} {1,-6} {2,3} {3,-15} {4,-15} {5, -15} {6,-15}\n",
                    count,
                    reservation.PNR,
                    reservation.Passengers.Count(),
                    reservation.FlightDate.ToShortDateString(),
                    flightDesignator,
                    originDestination,
                    flightTime
                );
                count++;
            }
            Console.WriteLine("\n-----------------------------------------------");
        }

        public void DisplayNoReservations()
        {
            ClearScreen();
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("No available reservations to be displayed.");
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
