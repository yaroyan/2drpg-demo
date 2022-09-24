using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public class EventStream
    {
        // version of the event stream returned
        public long Version;
        // all events in the stream
        public List<IEvent> Events = new List<IEvent>();
    }
}
