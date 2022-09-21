using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SeedWork.DDD.Application.CQRS
{
    public interface IEventBus
    {
        void Publish<T>(T events) where T : IEvent;
    }
}
