using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD4U.Domain.Model
{
    [Serializable]
    public abstract record EntityId(Guid Id) : ValueObject, IEntityId
    {
        public bool Equals(IEntityId other) => Equals(other as EntityId);
    }
}
