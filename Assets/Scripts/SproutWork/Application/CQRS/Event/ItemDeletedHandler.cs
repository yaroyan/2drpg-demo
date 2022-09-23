using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Event;
using Yaroyan.SproutWork.Domain.Event.Other;

namespace Yaroyan.SproutWork.Application.CQRS.Event
{
    public class ItemDeletedHandler : IEventHandler<ItemDeletedEvent>
    {
        public void Handle(ItemDeletedEvent events)
        {
            Debug.Log("Fire Event:" + events.Id);
        }
    }
}
