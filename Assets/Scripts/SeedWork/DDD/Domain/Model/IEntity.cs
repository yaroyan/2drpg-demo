using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public interface IEntity<T> : IEquatable<IEntity<T>> where T : IEntityId
    {
        T Id { get; }
    }
}
