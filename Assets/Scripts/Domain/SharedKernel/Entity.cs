using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

public abstract class Entity<T> : IEntity<T> where T : IEntityId
{
    public abstract T Id { get; }

    public bool Equals(IEntity<T> other)
    {
        throw new System.NotImplementedException();
    }
}
