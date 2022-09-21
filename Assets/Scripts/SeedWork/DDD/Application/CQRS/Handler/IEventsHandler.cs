using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SeedWork.DDD.Application.CQRS.Handler
{
    public interface IEventsHandler
    {

    }

    public interface IEventHandler<T> : IEventsHandler where T : IEvent
    {
        void Handle(T events);
    }
}
