using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public interface IAggregateRoot { }
    public interface IAggregateRoot<T> : IEntity<T>, IAggregateRoot where T : IEntityId { }
}
