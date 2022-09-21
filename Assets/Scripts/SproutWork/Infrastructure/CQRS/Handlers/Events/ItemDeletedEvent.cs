using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SproutWork.Application.CQRS
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
