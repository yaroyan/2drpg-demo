using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Domain.Model;
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

        protected virtual void Update<T, U>(T id, Func<IEnumerable<IEvent>, U> activator, Action<U> executor) where T : IEntityId where U : Entity<T>
        {
            EventStream stream =  _eventStore.LoadEventStream(id);
            U entity = activator(stream.Events);
            executor(entity);
            _eventStore.AppendToStream(id, stream.Version, entity.DomainEvents);
        }
    }
}
