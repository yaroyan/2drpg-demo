using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public class InMemorySnapshotRepository : ISnapshotRepository
    {
        readonly ConcurrentDictionary<IEntityId, (long Version, IAggregateRoot AggregateRoot)> _snapshot;
        public void SaveSnapshot<T>(IEntityId id, T snapshot, long version) where T : IAggregateRoot
        {
            _snapshot[id] = (version, snapshot);
        }

        public bool TryGetSnapshotById<T>(IEntityId id, out T snapshot, out long version) where T : IAggregateRoot
        {
            (long Version, IAggregateRoot Snapshot) value;
            bool result = _snapshot.TryGetValue(id, out value);
            version = value.Version;
            snapshot = (T)value.Snapshot;
            return result;
        }
    }
}
