namespace FlightReservation.Common.Types
{
    /// <summary>
    /// Enum class for specifying the time period of an age.
    /// </summary>
    public enum TimePeriod
    {
        Day,
        Year
    }

    /// <summary>
    /// Data type modeling an age of an entity (i.e. Person)
    /// </summary>
    public struct Age
    {
        #region Properties
        public int Value { get; set; }
        public TimePeriod TimePeriod { get; set; }
        #endregion

        #region Constructors
        public Age()
        {
            Value = 0;
            TimePeriod = TimePeriod.Day;
        }

        public Age(int value, TimePeriod timePeriod)
        {
            Value = value;
            TimePeriod = timePeriod;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            var timePeriodString = TimePeriod.ToString();

            if (Value > 1)
            {
                timePeriodString += "s";
            }

            return $"{Value} {timePeriodString}";
        }
        #endregion
    }
}
