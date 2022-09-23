using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Application.CQRS.Event
{
    public interface IEventBus
    {
        void Publish<T>(T events) where T : IEvent;
    }
}
