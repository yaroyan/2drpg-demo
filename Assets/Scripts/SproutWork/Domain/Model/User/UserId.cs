using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

namespace Yaroyan.Game.RPG.Domain.Model.User
{
    public record UserId(Guid Id) : EntityId(Id) { }
}
