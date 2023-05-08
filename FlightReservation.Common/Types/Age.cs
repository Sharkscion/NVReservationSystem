namespace FlightReservation.Common.Types
{
    public enum TimePeriod
    {
        Day,
        Year
    }

    public struct Age
    {
        public int Value { get; set; }
        public TimePeriod TimePeriod { get; set; }

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

        public override string ToString()
        {
            var timePeriodString = TimePeriod.ToString();

            if (Value > 1)
            {
                timePeriodString += "s";
            }

            return $"{Value} {timePeriodString}";
        }
    }
}
