using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    public record RouteId(string Id) : EntityId(Id) { }
}
