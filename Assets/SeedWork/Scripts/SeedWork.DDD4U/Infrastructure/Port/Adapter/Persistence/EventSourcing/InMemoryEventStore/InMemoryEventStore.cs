using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public class InMemoryEventStore : IAppendOnlyStore
    {
        readonly ConcurrentDictionary<string, List<StoredEvent>> _eventStore = new();
        List<StoredEvent> _all = new();

        public void Append(string Id, IEnumerable<StoredEvent> data, long expectedStreamVersion = -1)
        {

            var list = _eventStore.GetOrAdd(Id, s => new());
            if (expectedStreamVersion >= 0)
            {
                if (list.Count != expectedStreamVersion)
                    throw new AppendOnlyStoreConcurrencyException(expectedStreamVersion, list.Count, Id);
            }
            AddToCaches(Id, data);
        }

        void AddToCaches(string key, IEnumerable<StoredEvent> buffer)
        {
            _all.AddRange(buffer);
            _eventStore.AddOrUpdate(key, s => new(buffer), (s, records) => { records.AddRange(buffer); return records; });
        }

        public void Close() { }

        public void Dispose() { }

        public IEnumerable<StoredEvent> ReadRecords(string Id, long afterVersion, int maxCount)
        {
            List<StoredEvent> list;
            return _eventStore.TryGetValue(Id, out list) ? list : Enumerable.Empty<StoredEvent>();
        }

        public IEnumerable<StoredEvent> ReadRecords(long afterVersion, int maxCount) => _all.Skip((int)afterVersion).Take(maxCount);

        public string NextIdentity() => Guid.NewGuid().ToString();
    }
}
