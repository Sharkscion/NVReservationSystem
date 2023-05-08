using FlightReservation.Common.Contracts.Models;
using FlightReservation.Models.Flight;
using FlightReservation.Repositories;

namespace FlightReservation.Test.Repositories
{
    public class CachedFlightRepositoryShould
    {
        [Fact]
        public void Save_CreatedFlight()
        {
            var model = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
            };

            var repository = new CachedFlightRepository();

            bool successful = repository.Save(model);

            Assert.True(successful);
        }

        [Fact]
        public void Return_AllFlightsInMemory()
        {
            int count = 5;
            var repository = new CachedFlightRepository();

            while (count > 0)
            {
                var model = new FlightModel
                {
                    AirlineCode = "NV",
                    FlightNumber = count,
                    DepartureStation = "MNL",
                    ArrivalStation = "CEB",
                };

                repository.Save(model);
                count--;
            }

            IEnumerable<IFlight> flights = repository.List();

            Assert.True(flights.Count() == 5);
            Assert.Contains(flights, item => item.FlightNumber == 5);
        }
    }
}
