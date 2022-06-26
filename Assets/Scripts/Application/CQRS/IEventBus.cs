using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public interface IEventBus
    {
        void Publish<T>(T events) where T : IEvents;
    }
}
