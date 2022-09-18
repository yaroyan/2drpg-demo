using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public record EntityId(Guid Id) : ValueObject, IEntityId
    {
        public bool Equals(IEntityId other) => Equals(other as EntityId);
    }
}
