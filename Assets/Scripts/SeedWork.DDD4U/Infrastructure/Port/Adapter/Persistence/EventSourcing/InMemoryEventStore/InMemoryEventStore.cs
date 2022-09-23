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
        readonly ConcurrentDictionary<Guid, DataWithVersion[]> _eventStore = new();
        DataWithName[] _all = new DataWithName[0];

        public void Append(Guid Id, byte[] data, long expectedStreamVersion = -1)
        {

            var list = _eventStore.GetOrAdd(Id, s => new DataWithVersion[0]);
            if (expectedStreamVersion >= 0)
            {
                if (list.Length != expectedStreamVersion)
                    throw new AppendOnlyStoreConcurrencyException(expectedStreamVersion, list.Length, Id);
            }
            long commit = list.Length + 1;
            AddToCaches(Id, data, commit);
        }

        void AddToCaches(Guid key, byte[] buffer, long commit)
        {
            var record = new DataWithVersion(commit, buffer);
            _all = ImmutableAdd(_all, new DataWithName(key, buffer));
            _eventStore.AddOrUpdate(key, s => new[] { record }, (s, records) => ImmutableAdd(records, record));
        }

        static T[] ImmutableAdd<T>(T[] source, T item)
        {
            var copy = new T[source.Length + 1];
            Array.Copy(source, copy, source.Length);
            copy[source.Length] = item;
            return copy;
        }

        public void Close() { }

        public void Dispose() { }

        public IEnumerable<DataWithVersion> ReadRecords(Guid Id, long afterVersion, int maxCount)
        {
            DataWithVersion[] list;
            return _eventStore.TryGetValue(Id, out list) ? list : Enumerable.Empty<DataWithVersion>();
        }

        public IEnumerable<DataWithName> ReadRecords(long afterVersion, int maxCount) => _all.Skip((int)afterVersion).Take(maxCount);
    }
}
