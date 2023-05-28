using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Domain.Model
{
    public interface IAggregateRoot { }
    public interface IAggregateRoot<T> : IEntity<T>, IAggregateRoot where T : IEntityId { }
}
