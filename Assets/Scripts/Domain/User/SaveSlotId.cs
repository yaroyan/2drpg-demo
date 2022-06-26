using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

public record SaveSlotId(string id) : ValueObject, IEntityId { }
