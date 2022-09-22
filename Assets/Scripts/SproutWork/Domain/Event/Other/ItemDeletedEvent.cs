using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SproutWork.Domain.Event.Other
{
    public class ItemDeletedEvent : IEvent
    {
        public int Id { get; private set; }
        public ItemDeletedEvent(int id)
        {
            this.Id = id;
        }
    }
}
