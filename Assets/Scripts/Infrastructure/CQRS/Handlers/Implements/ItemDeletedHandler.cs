using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public class ItemDeletedHandler : IEventHandler<ItemDeletedEvent>
    {
        public void Handle(ItemDeletedEvent events)
        {
            Debug.Log("Fire Event:" + events.Id);
        }
    }
}
