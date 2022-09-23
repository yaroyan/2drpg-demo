using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// An entity that represents a path to a destination.
    /// </summary>
    public class Route : AggregateRoot<RouteId>, IAggregateRoot<RouteId>
    {
        public override RouteId Id { get; init; }
        LocationId _originId;
        public LocationId OriginId { get => _originId; private set => _originId = value ?? throw new ArgumentNullException(nameof(OriginId)); }
        LocationId _destinationId;
        public LocationId DestinationId { get => _destinationId; private set => _destinationId = value ?? throw new ArgumentNullException(nameof(DestinationId)); }
        TravelTime _time;
        public TravelTime Time { get => _time; private set => _time = value ?? throw new ArgumentNullException(nameof(Time)); }

        public Route(RouteId id, LocationId originId, LocationId destinationId, TravelTime time) : base(Enumerable.Empty<IEvent>())
        {
            Id = id;
            OriginId = originId;
            DestinationId = destinationId;
            Time = time;
        }

        public Route(IEnumerable<IEvent> events) : base(events) { }

        protected override void Mutate(IEvent @event)
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