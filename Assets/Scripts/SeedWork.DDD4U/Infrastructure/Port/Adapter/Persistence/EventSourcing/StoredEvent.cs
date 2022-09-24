using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public sealed record StoredEvent(string AggregateRootId, long Sequence, Type EventType, string EventBody, DateTime OccuredOn);
}
