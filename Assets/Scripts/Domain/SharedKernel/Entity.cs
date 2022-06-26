using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

public abstract class Entity<T> : IEntity<T>
{
    public abstract IEntityId Id { get; }

    public bool Equals(T other)
    {
        throw new System.NotImplementedException();
    }
}
