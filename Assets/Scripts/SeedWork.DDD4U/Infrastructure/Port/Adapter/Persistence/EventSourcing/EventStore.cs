using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using LiteDB;
using Yaroyan.SeedWork.Common.JSON;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    public class EventStore : IEventStore
    {
        readonly IAppendOnlyStore _appendOnlyStore;
        readonly IJsonMapper _jsonMapper;

        public EventStore(IAppendOnlyStore appendOnlyStore, IJsonMapper jsonMapper)
        {
            _appendOnlyStore = appendOnlyStore;
            _jsonMapper = jsonMapper;
        }

        string IdentityToGuid(IEntityId id) => id.Id;

        public EventStream LoadEventStream(IEntityId id) => LoadEventStream(id, 0, int.MaxValue);

        public EventStream LoadEventStream(IEntityId id, long skipEvents) => LoadEventStream(id, skipEvents, int.MaxValue);

        public EventStream LoadEventStream(IEntityId id, long skip, int take)
        {
            var name = IdentityToGuid(id);
            var records = _appendOnlyStore.ReadRecords(name, skip, take);
            List<IEvent> events = new();
            long version = default;
            foreach (var record in records)
            {
                events.Add(_jsonMapper.Deserialize<IEvent>(record.EventType, record.EventBody));
                version = record.Sequence;
            }
            var stream = new EventStream(version, events);
            return stream;
        }

        public T NextIdentity<T>(Func<string, T> generator) => generator(_appendOnlyStore.NextIdentity());

        public void AppendToStream(IEntityId id, long originalVersion, IEnumerable<IEvent> events)
        {
            if (!events.Any()) return;
            var name = IdentityToGuid(id);
            var data = events.Select(@event => new StoredEvent(id.Id, @event.Sequence, @event.GetType(), _jsonMapper.Serialize(@event), @event.OccuredOn));
            try
            {
                _appendOnlyStore.Append(name, data, originalVersion);
            }
            catch (AppendOnlyStoreConcurrencyException e)
            {
                // load server events
                var server = LoadEventStream(id, 0, int.MaxValue);
                // throw a real problem
                throw OptimisticConcurrencyException.Create(server.Version, e.ExpectedStreamVersion, id, server.Events);
            }
        }
    }
}
