using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Linq;
using VContainer;
using MessagePipe;
using UnityEngine;
using Yaroyan.SeedWork.DDD4U.Application;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing;

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
            builder.RegisterMessageBroker<ITestEvent>(options);
            builder.Register<ISnapshotRepository, InMemorySnapshotRepository>(Lifetime.Singleton);
            builder.Register<IAppendOnlyStore, InMemoryEventStore>(Lifetime.Singleton);
            builder.Register<IEventStore, EventStore>(Lifetime.Singleton);
            builder.Register<IApplicationService, TestApplicationService>(Lifetime.Singleton);
            _resolver = builder.Build();
            _applicationService = _resolver.Resolve<IApplicationService>();
            _subscriber = _resolver.Resolve<ISubscriber<ITestEvent>>();
            var bag = DisposableBag.CreateBuilder();
            _subscriber.Subscribe(e => Debug.Log(e.ToString())).AddTo(bag);
            _disposable = bag.Build();
            _appendOnlyStore = _resolver.Resolve<IAppendOnlyStore>();
            _eventStore = _resolver.Resolve<IEventStore>();
        }

        [Test]
        public void CreateInstance()
        {
            Assert.That(_applicationService is not null);
        }

        [Test]
        public void Persist()
        {
            _applicationService.Execute(new TestRegisteredCommand("test"));
            var data = _appendOnlyStore.ReadRecords(0, int.MaxValue).First();
            var stream = _eventStore.LoadEventStream(new TestEntityId(data.Id));
            var ar = new TestAggregateRoot(stream.Events);
            Debug.Log(stream.Events.First().ToString());
            Assert.That(ar is not null);
        }
    }
}
