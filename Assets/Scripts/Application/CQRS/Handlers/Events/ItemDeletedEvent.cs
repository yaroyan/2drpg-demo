using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public class ItemDeletedEvent : IEvents
    {
        public int Id { get; private set; }
        public ItemDeletedEvent(int id)
        {
            this.Id = id;
        }
    }
}
