﻿using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface IAddFlightView : IFormView
    {
        #region Properties
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public TimeOnly DepartureScheduledTime { get; set; }
        public TimeOnly ArrivalScheduledTime { get; set; }
        #endregion

        #region Events
        event EventHandler AirlineCodeChanged;
        event EventHandler FlightNumberChanged;
        event EventHandler ArrivalStationChanged;
        event EventHandler DepartureStationChanged;
        event EventHandler Submitted;
        #endregion
    }
}
