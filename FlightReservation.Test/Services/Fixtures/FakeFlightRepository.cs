using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Repositories;
using FlightReservation.Models.Flight;
using System.Text;

namespace FlightReservation.Test.Services.Fixtures
{
    public class FakeFlightRepository : IFlightRepository, IDisposable
    {
        private readonly HashSet<IFlight> _flights;

        public FakeFlightRepository()
        {
            _flights = new HashSet<IFlight>
            {
                new FlightModel
                {
                    AirlineCode = "NV",
                    FlightNumber = 1,
                    DepartureStation = "MNL",
                    ArrivalStation = "CEB",
                    DepartureScheduledTime = new TimeOnly(hour: 8, minute: 0),
                    ArrivalScheduledTime = new TimeOnly(hour: 9, minute: 0)
                },
                new FlightModel
                {
                    AirlineCode = "NV",
                    FlightNumber = 1,
                    DepartureStation = "MNL",
                    ArrivalStation = "CEB",
                    DepartureScheduledTime = new TimeOnly(hour: 14, minute: 0),
                    ArrivalScheduledTime = new TimeOnly(hour: 15, minute: 0)
                },
                new FlightModel
                {
                    AirlineCode = "NV",
                    FlightNumber = 1,
                    DepartureStation = "MNL",
                    ArrivalStation = "DVO",
                    DepartureScheduledTime = new TimeOnly(hour: 20, minute: 0),
                    ArrivalScheduledTime = new TimeOnly(hour: 21, minute: 30)
                },
                new FlightModel
                {
                    AirlineCode = "MK",
                    FlightNumber = 2,
                    DepartureStation = "MNL",
                    ArrivalStation = "SIN",
                    DepartureScheduledTime = new TimeOnly(hour: 20, minute: 0),
                    ArrivalScheduledTime = new TimeOnly(hour: 22, minute: 0)
                },
                new FlightModel
                {
                    AirlineCode = "MK",
                    FlightNumber = 3,
                    DepartureStation = "SIN",
                    ArrivalStation = "MNL",
                    DepartureScheduledTime = new TimeOnly(hour: 20, minute: 0),
                    ArrivalScheduledTime = new TimeOnly(hour: 22, minute: 0)
                },
            };
        }

        public void Dispose()
        {
            _flights.Clear();
        }

        public IQueryable<IFlight> List()
        {
            return _flights.AsQueryable();
        }

        public bool Save(IFlight item)
        {
            _flights.Append(item);
            return true;
        }
    }
}
