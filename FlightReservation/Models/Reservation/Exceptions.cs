namespace FlightReservation.Models.Reservation
{
    public class MoreThanMaxPassengerCountException : ArgumentOutOfRangeException
    {
        private const string DEFAULT_MESSAGE = "Max passenger count per booking has been reached.";

        public MoreThanMaxPassengerCountException()
            : base(DEFAULT_MESSAGE) { }

        public MoreThanMaxPassengerCountException(string? message)
            : base(message) { }

        public MoreThanMaxPassengerCountException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

    public class NoPassengersException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "Reservation does not have any passengers.";

        public NoPassengersException()
            : base(DEFAULT_MESSAGE) { }

        public NoPassengersException(string? message)
            : base(message) { }

        public NoPassengersException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

    public class InvalidFlightDateException : ArgumentOutOfRangeException
    {
        private const string DEFAULT_MESSAGE = "Flight date is invalid.";

        public InvalidFlightDateException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidFlightDateException(string? message)
            : base(message) { }

        public InvalidFlightDateException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

    public class FlightDoesNotExistException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "Flight does not exists.";

        public FlightDoesNotExistException()
            : base(DEFAULT_MESSAGE) { }

        public FlightDoesNotExistException(string? message)
            : base(message) { }

        public FlightDoesNotExistException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

    public class InvalidPNRException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "PNR is invalid.";

        public InvalidPNRException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidPNRException(string? message)
            : base(message) { }

        public InvalidPNRException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
