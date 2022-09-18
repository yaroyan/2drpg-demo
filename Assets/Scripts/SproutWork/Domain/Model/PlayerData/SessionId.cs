using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

namespace Yaroyan.Game.RPG.Domain.Model.SaveData
{
    public record SessionId(Guid Id) : EntityId(Id) { }
}
