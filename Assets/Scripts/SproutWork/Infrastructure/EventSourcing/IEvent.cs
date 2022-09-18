using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SproutWork.Infrastructure.EventSourcing
{
    public interface IEvent
    {
        public int EventVersion { get; }
        public System.DateTime OccuredOn { get; }
    }
}
