using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public sealed record StoredEvent(long EventId, Type EventType, string EventBody, DateTime DateTime);
}
