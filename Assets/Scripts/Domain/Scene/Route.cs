using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    /// <summary>
    /// An entity that represents a path to a destination.
    /// </summary>
    public class Route : IAggregateRoot<Route>
    {
        readonly RouteId _id;
        public IEntityId Id { get => _id; }
        LocationId _originId;
        public LocationId OriginId { get => _originId; private set => _originId = value ?? throw new ArgumentNullException(nameof(OriginId)); }
        LocationId _destinationId;
        public LocationId DestinationId { get => _destinationId; private set => _destinationId = value ?? throw new ArgumentNullException(nameof(DestinationId)); }
        TravelTime _time;
        public TravelTime Time { get => _time; private set => _time = value ?? throw new ArgumentNullException(nameof(Time)); }

        public Route(RouteId id, LocationId originId, LocationId destinationId, TravelTime time)
        {
            _id = id;
            OriginId = originId;
            DestinationId = destinationId;
            Time = time;
        }

        public bool Equals(Route other)
        {
            throw new NotImplementedException();
        }
    }

    public record TravelTime(int Hour) : ValueObject
    {
        static readonly int s_MinHour = 0;
        public int Hour { get; private init; } = Hour >= s_MinHour ? Hour : throw new ArgumentException($"{nameof(Hour)} must be positive or {s_MinHour}. Hour: {Hour}");
    }
}