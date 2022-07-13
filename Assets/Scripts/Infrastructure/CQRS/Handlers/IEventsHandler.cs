using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public interface IEventsHandler
    {

    }

    public interface IEventHandler<T> : IEventsHandler where T : IEvents
    {
        void Handle(T events);
    }
}
