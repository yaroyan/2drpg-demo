using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T> where T : IEntityId
    {

    }
}
