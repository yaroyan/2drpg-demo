using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public interface IEntityId : IEquatable<IEntityId>
    {
        Guid Id { get; init; }
    }
}
