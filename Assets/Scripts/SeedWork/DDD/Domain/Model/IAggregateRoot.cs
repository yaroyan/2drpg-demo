using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public interface IAggregateRoot<T> : IEntity<T> where T : IEntityId { }
}
