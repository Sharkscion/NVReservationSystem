using FlightReservation.Utilities;

namespace FlightReservation.Test.Utilities
{
    public class PNRGeneratorShould
    {
        #region Test Methods
        [Fact]
        public void CreatePNR_WhenValid()
        {
            string PNR = PNRGenerator.Generate(length: 6);
            Assert.Matches(@"^[A-Z][A-Z0-9]{5}$", PNR);
        }

        [Fact]
        public void CreateDifferentPNR_WhenGenerated()
        {
            string firstPNR = PNRGenerator.Generate(length: 6);
            string secondPNR = PNRGenerator.Generate(length: 6);

            Assert.NotEqual(secondPNR, firstPNR);
        }
        #endregion
    }
}
