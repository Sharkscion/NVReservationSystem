using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.UI.Views.Utilities
{
    internal class FlightPresenter
    {
        public static void DisplayFlights(IEnumerable<IFlight> flights)
        {
            Console.WriteLine("\n------------------------------------------------------------");
            var header = String.Format(
                "{0,3} {1,5} {2,-15} {3,-15} {4,-15}",
                "#",
                "Code",
                "Flight Number",
                "From -> To",
                "Flight Time"
            );
            Console.WriteLine(header);
            Console.WriteLine("------------------------------------------------------------");

            int count = 1;

            foreach (var flight in flights)
            {
                string output = String.Format(
                    "{0,3} {1,5} {2,-15} {3,-15} {4,-15}",
                    count,
                    flight.AirlineCode,
                    flight.FlightNumber,
                    $"{flight.DepartureStation} -> {flight.ArrivalStation}",
                    $"{flight.DepartureScheduledTime.ToString("HH:mm")} - {flight.ArrivalScheduledTime.ToString("HH:mm")}"
                );
                Console.WriteLine(output);

                count++;
            }
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
