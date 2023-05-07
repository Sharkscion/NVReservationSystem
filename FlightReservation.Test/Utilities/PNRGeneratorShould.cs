using FlightReservation.Utilities;

namespace FlightReservation.Test.Utilities
{
    public class PNRGeneratorShould
    {
        [Fact]
        public void Create_ValidPNR()
        {
            string PNR = PNRGenerator.Generate(length: 6);
            Assert.Matches(@"^[A-Z][A-Z0-9]{5}$", PNR);
        }

        [Fact]
        public void Create_UniquePNR()
        {
            string firstPNR = PNRGenerator.Generate(length: 6);
            string secondPNR = PNRGenerator.Generate(length: 6);

            Assert.NotEqual(secondPNR, firstPNR);
        }
    }
}
