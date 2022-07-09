using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;

public record SaveDataId(string id) : ValueObject, IEntityId { }
