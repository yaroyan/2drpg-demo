using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public interface ISnapshotRepository
    {
        bool TryGetSnapshotById<T>(IEntityId id, out T snapshot, out long version) where T : IAggregateRoot;
        void SaveSnapshot<T>(IEntityId id, T snapshot, long version) where T : IAggregateRoot;
    }
}
