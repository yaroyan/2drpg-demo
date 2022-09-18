using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.Game.RPG.Infrastructure.EventSourcing
{
    public class EventStream
    {
        public int Version { get; private set; }
        public List<IEvent> Events { get; private set; }
    }
}

