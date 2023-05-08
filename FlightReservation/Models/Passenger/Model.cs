using FlightReservation.Common.Validators;
using FlightReservation.Models.Contracts;

namespace FlightReservation.Models.Passenger
{
    public class PassengerModel : IPassenger
    {
        private readonly IDateTimeProvider? _dateTimeProvider;

        private string _firstName;
        private string _lastName;
        private DateTime _birthdate;
        private Age _age;

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

        public Age Age
        {
            get { return _age; }
        }

        public PassengerModel() { }

        public PassengerModel(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public PassengerModel(string firstName, string lastName, DateTime birthDate)
            : this()
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        private void calculateAge()
        {
            DateTime dateNow = _dateTimeProvider?.GetNow() ?? DateTime.Now;

            int daysInAYear = 365;

            if (isLeapYear(BirthDate.Year))
            {
                daysInAYear++;
            }

            int result = dateNow.Subtract(BirthDate).Days / daysInAYear;
            if (result > 0)
            {
                _age = new Age(value: result, timePeriod: TimePeriod.Year);
                return;
            }

            result = dateNow.Subtract(BirthDate).Days;
            _age = new Age(value: result, timePeriod: TimePeriod.Day);
        }

        private bool isLeapYear(int year)
        {
            return year % 4 == 0;
        }

        public IPassenger CreateFrom(string firstName, string lastName, DateTime birthDate)
        {
            return new PassengerModel(firstName, lastName, birthDate);
        }
    }
}
