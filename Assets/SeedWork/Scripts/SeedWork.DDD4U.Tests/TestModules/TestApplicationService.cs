using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository;
using System;
using MessagePipe;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using Yaroyan.SeedWork.DDD4U.Application;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Query;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Command;
using UnityEngine;
using VContainer;

namespace Yaroyan.SeedWork.DDD4U.Test
{
    public class TestApplicationService : ApplicationService
    {
        readonly IPublisher<ITestEvent> _publisher;
        [Inject]
        public TestApplicationService(IEventStore eventStore, ISnapshotRepository snapshotRepository, IPublisher<ITestEvent> publisher) : base(eventStore, snapshotRepository)
        {
            _publisher = publisher;
        }

        public override void Execute(ICommand command)
        {
            switch (command)
            {
                case TestChangeNameCommand _command:
                    When(_command);
                    break;
                case TestRegisteredCommand _command:
                    When(_command);
                    break;
                default:
                    throw new NotImplementedException($"Eventに対応する処理が未実装です。: {command.GetType()}");
            }
        }

        public void When(TestChangeNameCommand command) => Update<TestEntityId, TestAggregateRoot>(command.Id, domain => domain.ChangeName(command.Name));

        public void When(TestRegisteredCommand command) => Create<TestEntityId, TestAggregateRoot>(() => new TestAggregateRoot(_eventStore.NextIdentity(id => new TestEntityId(id)), command.Name));

        protected override void Publish(IEnumerable<IEvent> events)
        {
            foreach (ITestEvent @event in events) _publisher.Publish(@event);
        }
    }
}