using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public sealed record EventStream(long Version, IEnumerable<IEvent> Events);
}
