using FlightReservation.Common.Types;

namespace FlightReservation.Common.Contracts.Models
{
    public interface IPassenger
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Age Age { get; }
        #endregion

        #region Functions
        IPassenger CreateFrom(string firstName, string lastName, DateTime birthDate);

        #endregion
    }
}
