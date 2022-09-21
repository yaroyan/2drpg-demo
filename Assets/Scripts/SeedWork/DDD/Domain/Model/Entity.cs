using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public abstract class Entity<T> : IEntity<T> where T : IEntityId
    {
        public abstract T Id { get; init; }

        public Entity(IEnumerable<IEvent> events)
        {
            foreach (var @event in events) Mutate(@event);
        }

        // See link below.
        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation#the-deferred-approach-to-raise-and-dispatch-events
        List<IEvent> _domainEvents = new();
        public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IEvent @event) => _domainEvents.Add(@event);

        public void RemoveDomainEvent(IEvent @event) => _domainEvents?.Remove(@event);

        public void ClearDomainEvents() => _domainEvents?.Clear();

        protected void Apply(IEvent @event)
        {
            AddDomainEvent(@event);
            Mutate(@event);
        }

        protected abstract void Mutate(IEvent @event);
        //{
        //    switch(@event)
        //    {
        //        case XXXEvent xXXEvent:
        //            When(xXXEvent);
        //            break;
        //        default:
        //            throw new InvalidOperationException();
        //    }
        //}
        // If "dynamic" is available, the following should be implemented
        //=> ((dynamic)thid).When((dynamic)e);

        // Methods corresponding to events should be implemented in subclasses.
        // void When(XXXEvent @event);

        public override bool Equals(object obj) => Equals(obj as Entity<T>);

        public bool Equals(IEntity<T> obj) => Equals(obj as Entity<T>);

        public bool Equals(Entity<T> other) => other is not null && EqualityComparer<T>.Default.Equals(Id, other.Id);

        public override int GetHashCode() => HashCode.Combine(Id);

        public static bool operator ==(Entity<T> left, Entity<T> right) => EqualityComparer<Entity<T>>.Default.Equals(left, right);

        public static bool operator !=(Entity<T> left, Entity<T> right) => !(left == right);
    }
}
