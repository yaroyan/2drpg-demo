using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    /// <summary>
    /// Unique identifier of the Scene entity.
    /// </summary>
    public record SceneId(string Id) : ValueObject, IEntityId { }
}