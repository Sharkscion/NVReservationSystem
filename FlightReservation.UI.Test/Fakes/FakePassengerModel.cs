using FlightReservation.Common.Types;
using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.UI.Test.Fakes
{
    internal class FakePassengerModel : IPassenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public Age Age { get; }

        public FakePassengerModel() { }

        public FakePassengerModel(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public IPassenger CreateFrom(string firstName, string lastName, DateTime birthDate)
        {
            return new FakePassengerModel(firstName, lastName, birthDate);
        }
    }
}
