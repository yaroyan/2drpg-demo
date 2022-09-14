using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.Game.DDD.SharedKernel;

public record EntityId(Guid Id) : ValueObject, IEntityId { }
