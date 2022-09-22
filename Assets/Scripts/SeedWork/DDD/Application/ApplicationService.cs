using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Domain.Model;
using Yaroyan.SeedWork.DDD.Application.CQRS.Command;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository;

namespace Yaroyan.SeedWork.DDD.Application
{
    public abstract class ApplicationService : IApplicationService
    {
        protected readonly IEventStore _eventStore;
        protected readonly IUnitOfWork _unitOfWork;

        protected ApplicationService(IEventStore eventStore, IUnitOfWork unitOfWork)
        {
            _eventStore = eventStore;
            _unitOfWork = unitOfWork;
        }

        public abstract void Execute(ICommand command);

        protected virtual bool ConflictWith(IEvent e1, IEvent e2) => e1.GetType() == e2.GetType();

        protected virtual void Update<T, U>(T id, Func<IEnumerable<IEvent>, U> activator, Action<U> executor) where T : IEntityId where U : Entity<T>
        {
            EventStream stream = _eventStore.LoadEventStream(id);
            U entity = activator(stream.Events);
            executor(entity);
            try
            {
                _eventStore.AppendToStream(id, stream.Version, entity.DomainEvents);
            }
            catch (OptimisticConcurrencyException e)
            {
                foreach (var failedEvent in entity.DomainEvents)
                {
                    foreach (var successedEvent in e.ActualEvents)
                    {
                        if (!ConflictWith(failedEvent, successedEvent)) continue;
                        throw new RealConcurrencyException($@"Conflict between {failedEvent} and {successedEvent}", e);
                    }
                }
                _eventStore.AppendToStream(id, e.ActualVersion, entity.DomainEvents);
            }
        }
    }
}
