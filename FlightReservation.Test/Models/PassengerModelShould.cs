using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Types;
using FlightReservation.Models.Passenger;

namespace FlightReservation.Test.Models
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNow()
        {
            return new DateTime(2023, 5, 7);
        }
    }

    public class PassengerModelShould
    {
        #region Data Generators
        public static IEnumerable<object[]> GetInvalidNames()
        {
            yield return new object[] { null! };
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { "Name Space" };
            yield return new object[] { "Name1" };
            // More than 20 characters
            yield return new object[] { "abcdefghijklmnopqrstu" };
        }

        public static IEnumerable<object[]> GetValidNames()
        {
            yield return new object[] { "Name" };
            yield return new object[] { "abcdefghijklmnopqrst" };
        }

        public static IEnumerable<object[]> GetInvalidBirthDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddDays(-15) };
        }

        public static IEnumerable<object[]> GetValidBirthDates()
        {
            yield return new object[] { DateTime.Now.AddDays(-16) };
            yield return new object[] { DateTime.Now.AddYears(-18) };
        }

        public static IEnumerable<object[]> GetBirthDatesAndAgePairs()
        {
            yield return new object[] { DateTime.Now.AddDays(-16), "16 Days" };
            yield return new object[] { DateTime.Now.AddYears(-18), "18 Years" };
        }
        #endregion

        #region Test Methods

        [Theory]
        [MemberData(nameof(GetInvalidNames))]
        public void RaiseError_WhenInvalidFirstName(string value)
        {
            var model = new PassengerModel();

            Action action = () => model.FirstName = value;

            var exception = Assert.Throws<InvalidNameException>(action);
            Assert.Equal(nameof(model.FirstName), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidNames))]
        public void SetFirstName_WhenValid(string value)
        {
            var model = new PassengerModel();

            model.FirstName = value;

            Assert.Equal(value, model.FirstName);
        }

        [Theory]
        [MemberData(nameof(GetInvalidNames))]
        public void RaiseError_WhenInvalidLastName(string value)
        {
            var model = new PassengerModel();

            Action action = () => model.LastName = value;

            var exception = Assert.Throws<InvalidNameException>(action);
            Assert.Equal(nameof(model.LastName), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidNames))]
        public void SetLastName_WhenValid(string value)
        {
            var model = new PassengerModel();

            model.LastName = value;

            Assert.Equal(value, model.LastName);
        }

        [Theory]
        [MemberData(nameof(GetInvalidBirthDates))]
        public void RaiseError_WhenInvalidBirthDate(DateTime value)
        {
            var model = new PassengerModel();

            Action action = () => model.BirthDate = value;

            Assert.Throws<AgeLimitException>(action);
        }

        [Theory]
        [MemberData(nameof(GetValidBirthDates))]
        public void SetBirthDate_WhenValid(DateTime value)
        {
            var model = new PassengerModel();

            model.BirthDate = value;

            Assert.Equal(value, model.BirthDate);
        }

        [Theory]
        [MemberData(nameof(GetBirthDatesAndAgePairs))]
        public void CalculateCorrectAge_WhenBirthDateSupplied(
            DateTime birthDate,
            string expectedAgeString
        )
        {
            var model = new PassengerModel();
            model.BirthDate = birthDate;

            string actualAgeString = model.Age.ToString();

            Assert.Equal(actualAgeString, expectedAgeString);
        }

        [Fact]
        public void CalculateCorrectAge_WhenBirthDateHasLeapYear()
        {
            var dateTimeProvider = new DateTimeProvider();
            DateTime leapBirthDate = new DateTime(year: 1996, month: 2, day: 29);

            var model = new PassengerModel(dateTimeProvider);
            model.BirthDate = leapBirthDate;

            Age calculatedAge = model.Age;
            Age expectedAge = new Age(27, TimePeriod.Year);

            Assert.Equal(expectedAge, calculatedAge);
        }

        [Fact]
        public void CreateNewInstance_WhenDataProvided()
        {
            string firstName = "John";
            string lastName = "Doe";
            DateTime birthDate = DateTime.Now.AddYears(-18);

            var initModel = new PassengerModel();

            // Action
            var otherModel = initModel.CreateFrom(firstName, lastName, birthDate);

            // Assert
            Assert.Equal(firstName, otherModel.FirstName);
            Assert.Equal(lastName, otherModel.LastName);
            Assert.Equal(birthDate, otherModel.BirthDate);
        }
        #endregion
    }
}
