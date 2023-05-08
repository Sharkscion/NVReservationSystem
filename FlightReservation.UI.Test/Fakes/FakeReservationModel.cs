﻿using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.UI.Test.Fakes
{
    internal class FakeReservationModel : IReservation
    {
        public IFlight FlightInfo { get; set; }
        public DateTime FlightDate { get; set; }

        public string PNR { get; }

        public IEnumerable<IPassenger> Passengers { get; set; }

        public FakeReservationModel() { }

        public FakeReservationModel(string bookingReference)
        {
            PNR = bookingReference;
        }

        public FakeReservationModel(
            IFlight flightInfo,
            DateTime flightDate,
            IEnumerable<IPassenger> passengers
        )
        {
            FlightInfo = flightInfo;
            FlightDate = flightDate;
            Passengers = passengers;
        }

        public IReservation CreateWith(string bookingReference)
        {
            return new FakeReservationModel(bookingReference);
        }

        public IReservation CreateFrom(
            DateTime flightDate,
            IFlight flightInfo,
            IEnumerable<IPassenger> passengers
        )
        {
            return new FakeReservationModel(flightInfo, flightDate, passengers);
        }
    }
}
