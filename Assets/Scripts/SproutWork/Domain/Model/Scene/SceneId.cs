using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// Unique identifier of the Scene entity.
    /// </summary>
    public record SceneId : EntityId
    {
        public SceneId(Guid Id) : base(Id) => base.Id = Id;
    }
}