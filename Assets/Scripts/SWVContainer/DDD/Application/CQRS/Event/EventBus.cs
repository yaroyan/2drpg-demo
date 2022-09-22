using System.Collections;
using System.Collections.Generic;
using VContainer;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Application.CQRS.Event;
using Yaroyan.SeedWork.Common.VContainer;

namespace Yaroyan.SproutWork.Application.CQRS.Event
{
    public class EventBus : IEventBus
    {
        readonly IObjectResolverContractor<IEnumerable<IEventsHandler>> _resolver;
        [Inject]
        public EventBus(IObjectResolverContractor<IEnumerable<IEventsHandler>> resolver)
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
