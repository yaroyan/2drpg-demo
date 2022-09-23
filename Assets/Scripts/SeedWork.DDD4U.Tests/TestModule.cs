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

namespace Yaroyan.SeedWork.DDD4U.Test
{
    public class TestInMemoryUnitOfWork : IUnitOfWork
    {
        TestInMemoryRepository _testInMemoryRepository;
        public TestInMemoryRepository TestInMemoryRepository => _testInMemoryRepository ??= new TestInMemoryRepository();
        public void Commit() { }

        public void Dispose() { }

        public void Rollback() { }
    }
    public class TestInMemoryRepository : InMemoryRepository<TestEntityId, TestAggregateRoot>
    {
        public override TestEntityId NextIdentity() => new TestEntityId(Guid.NewGuid());
    }

    public class TestAggregateRoot : AggregateRoot<TestEntityId>
    {
        string _name;
        public string Name
        {
            get => _name;
            private set => _name = value ?? throw new ArgumentException(nameof(Name));
        }

        public override TestEntityId Id { get; init; }

        public TestAggregateRoot(IEnumerable<IEvent> events) : base(events) { }

        public TestAggregateRoot(TestEntityId id, string name) : base(new IEvent[] { }) {
            Id = id;
            _name = name;
        }

        public void ChangeName(string newName) => Apply(new TestChangeNameEvent(this.Id, newName));

        protected override void Mutate(IEvent @event)
        {
            switch (@event)
            {
                case TestChangeNameEvent _event:
                    When(_event);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        void When(TestChangeNameEvent @event)
        {
            Name = @event.Name;
        }
    }

    public sealed record TestEntityId(Guid Id) : EntityId(Id) { }
    public interface ITestEvent : IEvent { }
    public sealed record TestChangeNameEvent(TestEntityId Id, string Name) : ITestEvent { }
    public sealed record TestChangeNameCommand(TestEntityId Id, string Name) : ICommand { }

    public class TestApplicationService : ApplicationService
    {
        readonly IPublisher<ITestEvent> _publisher;
        public TestApplicationService(IEventStore eventStore, IUnitOfWork unitOfWork, ISnapshotRepository snapshotRepository, IPublisher<ITestEvent> publisher) : base(eventStore, unitOfWork, snapshotRepository)
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
                default:
                    break;
            }
        }

        public void When(TestChangeNameCommand command) => Update<TestEntityId, TestAggregateRoot>(command.Id, domain => domain.ChangeName(command.Name));

        protected override void Publish(IEnumerable<IEvent> events)
        {
            foreach (ITestEvent @event in events) _publisher.Publish(@event);
        }
    }
}
