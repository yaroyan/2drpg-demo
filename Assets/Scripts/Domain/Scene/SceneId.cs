using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;
using System;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    /// <summary>
    /// Unique identifier of the Scene entity.
    /// </summary>
    public record SceneId : EntityId
    {
        public SceneId(Guid Id) : base(Id) => base.Id = Id;
    }
}