﻿using FlightReservation.Common.Contracts.Models;
using System.Diagnostics.CodeAnalysis;

namespace FlightReservation.Comparers
{
    internal class FlightEqualityComparer : IEqualityComparer<IFlight>
    {
        #region Implementation of IEqualityComparer
        public bool Equals(IFlight? x, IFlight? y)
        {
            return x?.AirlineCode == y?.AirlineCode
                && x?.FlightNumber == y?.FlightNumber
                && x?.DepartureStation == y?.DepartureStation
                && x?.ArrivalStation == y?.ArrivalStation;
        }

        public int GetHashCode([DisallowNull] IFlight obj)
        {
            return obj.AirlineCode.GetHashCode()
                + obj.FlightNumber.GetHashCode()
                + obj.DepartureStation.GetHashCode()
                + obj.ArrivalStation.GetHashCode();
        }
        #endregion
    }
}
