using FlightReservation.Models;
using FlightReservation.Models.Passenger;
using FlightReservation.Utilities;

namespace FlightReservation.Test.Models
{
    public class PassengerModelShould
    {
        public static IEnumerable<object[]> GetInvalidNames()
        {
            yield return new object[] { null };
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

        [Theory]
        [MemberData(nameof(GetInvalidNames))]
        public void RaiseError_InvalidFirstName(string value)
        {
            var model = new PassengerModel();

            Action action = () => model.FirstName = value;

            var exception = Assert.Throws<InvalidNameException>(action);
            Assert.Equal(nameof(model.FirstName), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidNames))]
        public void Set_ValidFirstName(string value)
        {
            var model = new PassengerModel();

            model.FirstName = value;

            Assert.Equal(value, model.FirstName);
        }

        [Theory]
        [MemberData(nameof(GetInvalidNames))]
        public void RaiseError_InvalidLastName(string value)
        {
            var model = new PassengerModel();

            Action action = () => model.LastName = value;

            var exception = Assert.Throws<InvalidNameException>(action);
            Assert.Equal(nameof(model.LastName), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidNames))]
        public void Set_ValidLastName(string value)
        {
            var model = new PassengerModel();

            model.LastName = value;

            Assert.Equal(value, model.LastName);
        }

        public static IEnumerable<object[]> GetInvalidBirthDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddDays(-15) };
        }

        [Theory]
        [MemberData(nameof(GetInvalidBirthDates))]
        public void RaiseError_InvalidBirthDate(DateTime value)
        {
            var model = new PassengerModel();

            Action action = () => model.BirthDate = value;

            Assert.Throws<AgeLimitException>(action);
        }

        public static IEnumerable<object[]> GetValidBirthDates()
        {
            yield return new object[] { DateTime.Now.AddDays(-16) };
            yield return new object[] { DateTime.Now.AddYears(-18) };
        }

        [Theory]
        [MemberData(nameof(GetValidBirthDates))]
        public void Set_ValidBirthDate(DateTime value)
        {
            var model = new PassengerModel();

            model.BirthDate = value;

            Assert.Equal(value, model.BirthDate);
        }

        public static IEnumerable<object[]> GetBirthDatesAndAgePairs()
        {
            yield return new object[] { DateTime.Now.AddDays(-16), "16 Days" };
            yield return new object[] { DateTime.Now.AddYears(-18), "18 Years" };
        }

        [Theory]
        [MemberData(nameof(GetBirthDatesAndAgePairs))]
        public void CalculateAge_Correctly(DateTime birthDate, string expectedAgeString)
        {
            var model = new PassengerModel();
            model.BirthDate = birthDate;

            string actualAgeString = model.Age.ToString();

            Assert.Equal(actualAgeString, expectedAgeString);
        }

        [Fact]
        public void CalculateAge_WithLeapYearBirthDate_Correctly()
        {
            var dateTimeProvider = new DateTimeProvider();
            dateTimeProvider.DateNow = new DateTime(2023, 5, 7);

            DateTime leapBirthDate = new DateTime(year: 1996, month: 2, day: 29);

            var model = new PassengerModel(dateTimeProvider);
            model.BirthDate = leapBirthDate;

            Age calculatedAge = model.Age;
            Age expectedAge = new Age(27, TimePeriod.Year);

            Assert.Equal(expectedAge, calculatedAge);
        }
    }
}
