using System.Collections;
using System.Collections.Generic;
using VContainer;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public class EventBus : IEventBus
    {
        readonly IObjectResolver _resolver;
        [Inject]
        public EventBus(IObjectResolver resolver)
        {
            this._resolver = resolver;
        }

        public void Publish<T>(T events) where T : IEvents
        {
            var handlers = this._resolver.Resolve<IEnumerable<IEventHandler<T>>>();
            foreach (var handler in handlers) handler.Handle(events);
        }
    }
}
