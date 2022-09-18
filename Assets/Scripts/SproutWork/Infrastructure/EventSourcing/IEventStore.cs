using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.Game.RPG.Infrastructure.EventSourcing
{
    public interface IEventStore
    {
        EventStream LoadEventStream(IEntityId id);
        EventStream LoadEventStream(IEntityId id, int skipEvents, int maxCount);
        void AppendToStream(IEntityId id, int expectedVersion, ICollection<IEvent> events);
    }
}

