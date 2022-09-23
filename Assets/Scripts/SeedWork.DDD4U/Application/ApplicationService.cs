using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Command;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing;

namespace Yaroyan.SeedWork.DDD4U.Application
{
    public abstract class ApplicationService : IApplicationService
    {
        static readonly ConcurrentDictionary<Type, Func<IEnumerable<IEvent>, IAggregateRoot>> s_cachedInstantiationExpression = new();
        protected readonly ISnapshotRepository _snapshotRepository;
        protected readonly IEventStore _eventStore;

        protected ApplicationService(IEventStore eventStore, ISnapshotRepository snapshotRepository)
        {
            _eventStore = eventStore;
            _snapshotRepository = snapshotRepository;
        }

        public abstract void Execute(ICommand command);

        protected virtual bool ConflictWith(IEvent e1, IEvent e2) => e1.GetType() == e2.GetType();

        protected virtual void Update<T, U>(T id, Action<U> executor) where T : IEntityId where U : AggregateRoot<T>
        {
            (long version, U aggregateRoot) = LoadAggregateRootWithVersionById<T, U>(id);
            executor(aggregateRoot);
            try
            {
                _eventStore.AppendToStream(id, version, aggregateRoot.DomainEvents);
                Publish(aggregateRoot.DomainEvents);
            }
            catch (OptimisticConcurrencyException e)
            {
                foreach (var failedEvent in aggregateRoot.DomainEvents)
                {
                    foreach (var successedEvent in e.ActualEvents)
                    {
                        if (!ConflictWith(failedEvent, successedEvent)) continue;
                        throw new RealConcurrencyException($@"Conflict between {failedEvent} and {successedEvent}", e);
                    }
                }
                _eventStore.AppendToStream(id, e.ActualVersion, aggregateRoot.DomainEvents);
                Publish(aggregateRoot.DomainEvents);
            }
        }

        protected virtual void Create<T, U>(Func<U> instantiator) where T : IEntityId where U : AggregateRoot<T> {
            var aggregateRoot = instantiator.Invoke();
            _eventStore.AppendToStream(aggregateRoot.Id, -1, aggregateRoot.DomainEvents);
            Publish(aggregateRoot.DomainEvents);
        }

        protected abstract void Publish(IEnumerable<IEvent> events);

        protected virtual U LoadAggregateRootById<T, U>(T id) where T : IEntityId where U : AggregateRoot<T> => LoadAggregateRootWithVersionById<T, U>(id).AggregateRoot;

        /// <summary>
        /// If performance issues are a concern, define instance creation methods in subclasses.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual (long Version, U AggregateRoot) LoadAggregateRootWithVersionById<T, U>(T id) where T : IEntityId where U : AggregateRoot<T>
        {
            U aggregateRoot;
            long snapshotVersion;
            if (_snapshotRepository.TryGetSnapshotById(id, out aggregateRoot, out snapshotVersion))
            {
                var stream = _eventStore.LoadEventStream(id, snapshotVersion);
                aggregateRoot.ReplayEvents(stream.Events);
                return (Version: stream.Version, AggregateRoot: aggregateRoot);
            }
            else
            {
                var stream = _eventStore.LoadEventStream(id);
                return (Version: stream.Version, AggregateRoot: (U)GetInstantiationExpression<U>()(stream.Events));
            }
        }

        Func<IEnumerable<IEvent>, IAggregateRoot> GetInstantiationExpression<T>()
            => s_cachedInstantiationExpression.GetOrAdd(
                typeof(T),
                Expression.Lambda<Func<IEnumerable<IEvent>, IAggregateRoot>>(Expression.New(typeof(T).GetConstructor(new[] { typeof(IEnumerable<IEvent>) }), Expression.Parameter(typeof(IEnumerable<IEvent>), "events"))).Compile());

        protected virtual void GenerateSnapshot<T, U>(T id) where T : IEntityId where U : AggregateRoot<T>
        {
            (long version, U aggregateRoot) = LoadAggregateRootWithVersionById<T, U>(id);
            _snapshotRepository.SaveSnapshot(id, aggregateRoot, version);
        }
    }
}
