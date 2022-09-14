using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.SaveData
{
    public record SessionId(string id) : ValueObject, IEntityId { }
}
