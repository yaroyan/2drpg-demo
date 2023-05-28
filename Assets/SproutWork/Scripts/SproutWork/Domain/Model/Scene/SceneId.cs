using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// Unique identifier of the Scene entity.
    /// </summary>
    public record SceneId(string id) : EntityId(id);
}