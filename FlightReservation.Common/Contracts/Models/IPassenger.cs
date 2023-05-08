
using FlightReservation.Common.Types;

namespace FlightReservation.Common.Contracts.Models
{
    public interface IPassenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Age Age { get; }

        IPassenger CreateFrom(string firstName, string lastName, DateTime birthDate);
    }
}
