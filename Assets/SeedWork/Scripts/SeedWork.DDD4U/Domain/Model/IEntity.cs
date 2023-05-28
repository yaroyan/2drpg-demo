using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD4U.Domain.Model
{
    public interface IEntity { }
    public interface IEntity<T> : IEntity, IEquatable<IEntity<T>> where T : IEntityId
    {
        T Id { get; }
    }
}
