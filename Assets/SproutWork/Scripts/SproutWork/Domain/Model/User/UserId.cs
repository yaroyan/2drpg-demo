using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System;

namespace Yaroyan.SproutWork.Domain.Model.User
{
    public record UserId(string Id) : EntityId(Id) { }
}
