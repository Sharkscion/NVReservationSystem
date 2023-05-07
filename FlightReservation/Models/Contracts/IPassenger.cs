namespace FlightReservation.Models.Contracts
{
    public interface IPassenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Age Age { get; }
    }
}
