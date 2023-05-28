using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Domain.Event
{
    public interface IEvent
    {
        string Id { get; init; }
        long Sequence { get; init; }
        DateTime OccurredOn { get; init; }
    }
}
