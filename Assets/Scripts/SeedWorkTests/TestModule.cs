using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository;
using System;
using System.Linq;
using Yaroyan.SeedWork.DDD.Domain.Model;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Application;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.EventSourcing;

namespace Yaroyan.SeedWork.DDD.Test
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

        public TestAggregateRoot(TestEntityId id, string name) : base(Enumerable.Empty<IEvent>()) { }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

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
    public sealed record TestChangeNameEvent(TestEntityId Id, string Name) : IEvent { }
    public sealed record TestChangeNameCommand(TestEntityId Id, string Name) : ICommand { }

    public class TestApplicationService : ApplicationService
    {
        readonly TestInMemoryUnitOfWork _unitOfWork2;

        public TestApplicationService(IEventStore eventStore, TestInMemoryUnitOfWork unitOfWork) : base(eventStore, unitOfWork)
        {
            _unitOfWork2 = unitOfWork;
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

        public void When(TestChangeNameCommand command) => Update(command.Id, domain => domain.ChangeName(command.Name));

        void Update(TestEntityId id, Action<TestAggregateRoot> action) => Update(id, events => new TestAggregateRoot(events), action);

        public TestAggregateRoot LoadById(TestEntityId id) => new TestAggregateRoot(_eventStore.LoadEventStream(id).Events);
    }
}
