using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    /// <summary>
    /// Unique identifier of the Location entity.
    /// </summary>
    public record LocationId(string Id) : ValueObject, IEntityId { }
}
