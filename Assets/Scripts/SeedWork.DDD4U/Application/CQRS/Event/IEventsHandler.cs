using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Application.CQRS.Event
{
    public interface IEventsHandler
    {

    }

    public interface IEventHandler<T> : IEventsHandler where T : IEvent
    {
        void Handle(T events);
    }
}
