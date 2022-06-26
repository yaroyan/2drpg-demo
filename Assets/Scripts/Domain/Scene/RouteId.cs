using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    public record RouteId(string Id) : ValueObject, IEntityId { }
}
