using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.Game.RPG.Infrastructure.EventSourcing
{
    public interface IEvent
    {
        public int EventVersion { get; }
        public System.DateTime OccuredOn { get; }
    }
}
