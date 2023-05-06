namespace FlightReservation.Data.Contracts
{
    public interface IPassenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; }
    }
}
