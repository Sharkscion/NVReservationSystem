namespace FlightReservation.Utilities
{
    public static class PNRGenerator
    {
        #region Functions
        /// <summary>
        /// Generates an alphanumeric random string.
        /// </summary>
        /// <param name="length">Lenght of the string to be generated.</param>
        public static string Generate(int length)
        {
            char[] generatedCode = new char[length];

            Func<char>[] generators =
            {
                GenerateRandomLetter,
                GenerateRandomNumericCharacter,
            };

            Random randomGeneratorIndex = new Random();

            generatedCode[0] = GenerateRandomLetter();

            for (int i = 1; i < length; i++)
            {
                Func<char> generator = generators[randomGeneratorIndex.Next(generators.Length)];
                generatedCode[i] = generator();
            }

            return new string(generatedCode);
        }

        /// <summary>
        /// Generates a random letter.
        /// </summary>
        public static char GenerateRandomLetter()
        {
            int lowerBound = 65;
            int upperBound = 90;

            Random random = new Random();
            return (char)random.Next(lowerBound, upperBound + 1);
        }

        /// <summary>
        /// Generates a random numeric character.
        /// </summary>
        public static char GenerateRandomNumericCharacter()
        {
            int lowerBound = 48;
            int upperBound = 57;

            Random random = new Random();
            return (char)random.Next(lowerBound, upperBound + 1);
        }
        #endregion
    }
}
