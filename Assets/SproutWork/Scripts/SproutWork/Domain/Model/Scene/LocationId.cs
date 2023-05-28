using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// Unique identifier of the Location entity.
    /// </summary>
    public record LocationId(string Id) : EntityId(Id) { }
}
