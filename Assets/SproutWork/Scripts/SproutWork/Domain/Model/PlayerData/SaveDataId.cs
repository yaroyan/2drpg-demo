using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System;

public record SaveDataId(string Id) : EntityId(Id) { }
