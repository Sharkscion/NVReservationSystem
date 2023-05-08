﻿using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Repositories;

namespace FlightReservation.Repositories
{
    public class CachedFlightRepository : IFlightRepository
    {
        private readonly HashSet<IFlight> _flights;

        public CachedFlightRepository()
        {
            _flights = new HashSet<IFlight>();
        }

        public IQueryable<IFlight> List()
        {
            return _flights.AsQueryable();
        }

        public bool Save(IFlight item)
        {
            _flights.Add(item);
            return true;
        }
    }
}
