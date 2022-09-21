using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T> where T : IEntityId
    {
        protected AggregateRoot(IEnumerable<IEvent> events) : base(events) { }
    }
}
