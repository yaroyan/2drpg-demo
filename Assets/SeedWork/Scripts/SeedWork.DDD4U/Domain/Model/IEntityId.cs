using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD4U.Domain.Model
{
    public interface IEntityId : IEquatable<IEntityId>
    {
        string Id { get; init; }
    }
}
