using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.User
{
    public record UserId(string id) : ValueObject, IEntityId { }
}