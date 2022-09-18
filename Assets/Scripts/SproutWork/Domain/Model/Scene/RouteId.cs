using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    public record RouteId(Guid Id) : EntityId(Id) { }
}
