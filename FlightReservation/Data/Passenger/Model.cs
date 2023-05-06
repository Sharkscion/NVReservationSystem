using FlightReservation.Common.Validators;
using FlightReservation.Data.Contracts;

namespace FlightReservation.Data.Passenger
{
    public class PassengerModel : IPassenger
    {
        private string _firstName;
        private string _lastName;
        private DateTime _birthdate;
        private int _age;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (!PassengerValidator.IsNameValid(value))
                {
                    throw new InvalidNameException(
                        paramName: nameof(FirstName),
                        message: "Name must be at least 20 letters."
                    );
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (!PassengerValidator.IsNameValid(value))
                {
                    throw new InvalidNameException(
                        paramName: nameof(LastName),
                        message: "Name must be at least 20 letters."
                    );
                }

                _lastName = value;
            }
        }

        public DateTime BirthDate
        {
            get { return _birthdate; }
            set
            {
                if (!PassengerValidator.IsBirthDateValid(value))
                {
                    throw new AgeLimitException("Passenger should at least be 16 days old.");
                }

                _birthdate = value;
                calculateAge();
            }
        }

        public int Age
        {
            get { return _age; }
        }

        public PassengerModel(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        private void calculateAge()
        {
            _age = DateTime.UtcNow.AddYears(-BirthDate.Year).Year;
        }
    }
}
