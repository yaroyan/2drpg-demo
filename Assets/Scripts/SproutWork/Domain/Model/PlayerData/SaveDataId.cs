using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System;

public record SaveDataId(Guid Id) : EntityId(Id) { }
