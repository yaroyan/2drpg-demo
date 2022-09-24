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
        InMemoryRepository<TestEntityId, TestAggregateRoot> _testInMemoryRepository;
        public InMemoryRepository<TestEntityId, TestAggregateRoot> TestInMemoryRepository => _testInMemoryRepository ??= new InMemoryRepository<TestEntityId, TestAggregateRoot>();
        public void Commit() { }

        public void Dispose() { }

        public void Rollback() { }
    }

    public class TestAggregateRoot : AggregateRoot<TestEntityId>
    {
        string _name;
        public string Name
        {
            get => _name;
            private set => _name = value ?? throw new ArgumentException(nameof(Name));
        }
        TestEntityId _id;
        public override TestEntityId Id { get => _id; }

        public TestAggregateRoot(IEnumerable<IEvent> events) : base(events) { }

        public TestAggregateRoot(TestEntityId id, string name) => Apply(new TestRegisteredEvent(id.Id, ++_version, DateTime.Now, name));

        public void ChangeName(string newName) => Apply(new TestChangeNameEvent(this.Id.Id, ++_version, DateTime.Now, newName));

        protected override void Mutate(IEvent @event)
        {
            _version = @event.Sequence;
            switch (@event)
            {
                case TestChangeNameEvent _event:
                    When(_event);
                    break;
                case TestRegisteredEvent _event:
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

        void When(TestRegisteredEvent @event)
        {
            _id = new TestEntityId(@event.AggregateRootId);
            Name = @event.Name;
        }
    }

    [Serializable]
    public sealed record TestEntityId(string Id) : EntityId(Id) { }
    public interface ITestEvent : IEvent { }
    [Serializable]
    public sealed record TestRegisteredCommand(string Name) : ICommand;
    [Serializable]
    public sealed record TestRegisteredEvent(string AggregateRootId, long Sequence, DateTime OccuredOn, string Name) : Event(AggregateRootId, Sequence, OccuredOn), ITestEvent;
    [Serializable]
    public sealed record TestChangeNameEvent(string AggregateRootId, long Sequence, DateTime OccuredOn, string Name) : Event(AggregateRootId, Sequence, OccuredOn), ITestEvent;
    [Serializable]
    public sealed record TestChangeNameCommand(TestEntityId Id, string Name) : ICommand;
}

