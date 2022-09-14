using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.Game.DDD.SharedKernel
{
    public interface IAggregateRoot<T> : IEntity<T> where T : IEntityId { }
}
