using System.Collections;
using System.Collections.Generic;
using VContainer;
using Yaroyan.SeedWork.DDD.Application.CQRS;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Application.CQRS.Handler;

namespace Yaroyan.SproutWork.Application.CQRS
{
    public class EventBus : IEventBus
    {
        readonly IObjectResolver _resolver;
        [Inject]
        public EventBus(IObjectResolver resolver)
        {
            this._resolver = resolver;
        }

        public void Publish<T>(T events) where T : IEvent
        {
            var handlers = this._resolver.Resolve<IEnumerable<IEventHandler<T>>>();
            foreach (var handler in handlers) handler.Handle(events);
        }
    }
}
