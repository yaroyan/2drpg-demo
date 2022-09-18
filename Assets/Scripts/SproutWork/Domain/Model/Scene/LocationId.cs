using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    /// <summary>
    /// Unique identifier of the Location entity.
    /// </summary>
    public record LocationId(Guid Id) : EntityId(Id) { }
}
