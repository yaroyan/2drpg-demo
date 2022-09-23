using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Domain.Model
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T> where T : IEntityId
    {
        protected AggregateRoot()
        {

        }

        public AggregateRoot(IEnumerable<IEvent> events)
        {
            foreach (var @event in events) Mutate(@event);
        }

        // See link below.
        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation#the-deferred-approach-to-raise-and-dispatch-events
        List<IEvent> _domainEvents = new();
        public IEnumerable<IEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IEvent @event) => _domainEvents.Add(@event);

        public void RemoveDomainEvent(IEvent @event) => _domainEvents?.Remove(@event);

        public void ClearDomainEvents() => _domainEvents?.Clear();

        protected void Apply(IEvent @event)
        {
            AddDomainEvent(@event);
            Mutate(@event);
        }

        protected abstract void Mutate(IEvent @event);

        public void ReplayEvents(List<IEvent> events)
        {
            foreach (var @event in events) Mutate(@event);
        }
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
    }
}
