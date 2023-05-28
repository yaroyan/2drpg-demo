using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Linq;
using VContainer;
using LiteDB;
using MessagePipe;
using UnityEngine;
using Yaroyan.SeedWork.DDD4U.Application;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing;
using Yaroyan.SeedWork.Common.JSON;
using Yaroyan.SeedWork.DDD4U.Common;
using System.Linq.Expressions;

namespace Yaroyan.SeedWork.DDD4U.Test
{
    public class EventSourcingTest : IDisposable
    {
        IObjectResolver _resolver;
        IApplicationService _applicationService;
        ISubscriber<ITestEvent> _subscriber;
        IDisposable _disposable;
        IAppendOnlyStore _appendOnlyStore;
        IEventStore _eventStore;

        public void Dispose()
        {
            _disposable.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            var options = builder.RegisterMessagePipe();
            builder.Register<IJsonSerializer, SeedWork.Common.JSON.JsonSerializer>(Lifetime.Singleton);
            builder.Register<IJsonDeserializer, JsonDeserializer>(Lifetime.Singleton);
            builder.Register<IJsonMapper, JsonMapper>(Lifetime.Singleton);
            builder.RegisterMessageBroker<ITestEvent>(options);
            builder.Register<ISnapshotRepository, InMemorySnapshotRepository>(Lifetime.Singleton);
            builder.Register<IAppendOnlyStore, InMemoryEventStore>(Lifetime.Singleton);
            builder.Register<IEventStore, EventStore>(Lifetime.Singleton);
            builder.Register<IApplicationService, TestApplicationService>(Lifetime.Singleton);
            _resolver = builder.Build();
            _applicationService = _resolver.Resolve<IApplicationService>();
            _subscriber = _resolver.Resolve<ISubscriber<ITestEvent>>();
            var bag = DisposableBag.CreateBuilder();
            _subscriber.Subscribe(e => Debug.Log($"subscribed:{e}")).AddTo(bag);
            _disposable = bag.Build();
            _appendOnlyStore = _resolver.Resolve<IAppendOnlyStore>();
            _eventStore = _resolver.Resolve<IEventStore>();
        }

        [Test]
        public void Persist()
        {
            _applicationService.Execute(new TestRegisteredCommand("test"));
            var data = _appendOnlyStore.ReadRecords(0, int.MaxValue).First();
            Debug.Log(data.ToString());
            var stream = _eventStore.LoadEventStream(new TestEntityId(data.Id));
            Debug.Log(stream.ToString());
            var ar = new TestAggregateRoot(stream.Events);
            Debug.Log(stream.Events.First().ToString());
            Assert.That(ar is not null);
        }

        [Test]
        public void InsertBSONIntoCollection()
        {
            var @event = new TestRegisteredEvent(Guid.NewGuid().ToString(), 0, DateTime.Now, "test");
            var document = LiteDB.JsonSerializer.Deserialize(_resolver.Resolve<IJsonMapper>().Serialize(@event));
            var filename = System.IO.Path.Combine(UnityEngine.Application.dataPath, "Addressables/Database/LiteDB/EventStore/EventStore.bytes");
            using (var db = new LiteDatabase($@"filename={filename}; password=admin"))
            {
                var col = db.GetCollection("events");
                col.Insert(document.AsDocument);
                var records = col.FindAll();
                foreach (var record in records) Debug.Log(record);
                // データを全て削除する
                col.DeleteAll();
            }
        }
    }
}
