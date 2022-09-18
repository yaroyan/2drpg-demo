using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

namespace Yaroyan.SproutWork.Domain.Model.User
{
    public record UserId(Guid Id) : EntityId(Id) { }
}
