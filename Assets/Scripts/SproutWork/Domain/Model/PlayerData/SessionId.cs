using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

namespace Yaroyan.SproutWork.Domain.Model.SaveData
{
    public record SessionId(Guid Id) : EntityId(Id) { }
}
