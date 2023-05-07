namespace FlightReservation.Utilities
{
    public static class PNRGenerator
    {
        delegate char CharGenerator();

        public static string Generate(int length)
        {
            char[] generatedCode = new char[length];

            CharGenerator[] generators =
            {
                new CharGenerator(GenerateRandomLetter),
                new CharGenerator(GenerateRandomNumericCharacter),
            };

            Random randomGeneratorIndex = new Random();

            generatedCode[0] = GenerateRandomLetter();

            for (int i = 1; i < length; i++)
            {
                CharGenerator generator = generators[randomGeneratorIndex.Next(generators.Length)];
                generatedCode[i] = generator();
            }

            return new string(generatedCode);
        }

        public static char GenerateRandomLetter()
        {
            int lowerBound = 65;
            int upperBound = 90;

            Random random = new Random();
            return (char)random.Next(lowerBound, upperBound + 1);
        }

        public static char GenerateRandomNumericCharacter()
        {
            int lowerBound = 48;
            int upperBound = 57;

            Random random = new Random();
            return (char)random.Next(lowerBound, upperBound + 1);
        }
    }
}
