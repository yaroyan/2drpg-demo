using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public interface IEventStore
    {
        EventStream LoadEventStream(IEntityId id);
        EventStream LoadEventStream(IEntityId id, long skipEvents);
        EventStream LoadEventStream(IEntityId id, long skipEvents, int maxCount);
        /// <summary>
        /// Appends events to server stream for the provided identity.
        /// </summary>
        /// <param name="id">identity to append to.</param>
        /// <param name="expectedVersion">The expected version (specify -1 to append anyway).</param>
        /// <param name="events">The events to append.</param>
        /// <exception cref="OptimisticConcurrencyException">when new events were added to server
        /// since <paramref name="expectedVersion"/>
        /// </exception>
        void AppendToStream(IEntityId id, long expectedVersion, IEnumerable<IEvent> events);
        T NextIdentity<T>(Func<string, T> generator);
    }
}
