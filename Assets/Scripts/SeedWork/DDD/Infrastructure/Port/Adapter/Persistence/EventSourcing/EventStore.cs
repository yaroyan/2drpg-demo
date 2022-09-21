using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    /// <summary>
    /// Actual implementation of <see cref="IEventStore"/>, which deals with
    /// serialization and naming in order to provide bridge between event-centric
    /// domain code and byte-based append-only persistence
    /// </summary>
    public class EventStore : IEventStore
    {
        readonly BinaryFormatter _formatter = new BinaryFormatter();

        byte[] SerializeEvent(IEvent[] e)
        {
            using (var mem = new MemoryStream())
            {
                _formatter.Serialize(mem, e);
                return mem.ToArray();
            }
        }

        IEvent[] DeserializeEvent(byte[] data)
        {
            using (var mem = new MemoryStream(data))
            {
                return (IEvent[])_formatter.Deserialize(mem);
            }
        }

        public EventStore(IAppendOnlyStore appendOnlyStore)
        {
            _appendOnlyStore = appendOnlyStore;
        }

        readonly IAppendOnlyStore _appendOnlyStore;
        public EventStream LoadEventStream(IEntityId id, long skip, int take)
        {
            var name = IdentityToGuid(id);
            var records = _appendOnlyStore.ReadRecords(name, skip, take).ToList();
            var stream = new EventStream();

            foreach (var tapeRecord in records)
            {
                stream.Events.AddRange(DeserializeEvent(tapeRecord.Data));
                stream.Version = tapeRecord.Version;
            }
            return stream;
        }

        Guid IdentityToGuid(IEntityId id) => id.Id;

        public EventStream LoadEventStream(IEntityId id)
        {
            return LoadEventStream(id, 0, int.MaxValue);
        }

        public void AppendToStream(IEntityId id, long originalVersion, IEnumerable<IEvent> events)
        {
            if (!events.Any())
                return;
            var name = IdentityToGuid(id);
            var data = SerializeEvent(events.ToArray());
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

            // technically there should be parallel process that queries new changes from 
            // event store and sends them via messages. 
            // however, for demo purposes, we'll just send them to console from here

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (var @event in events)
            {
                Console.WriteLine("  {0} r{1} Event: {2}", id, originalVersion, @event);
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
    }
}
