using VContainer;
using VContainer.Unity;
using UnityEngine;
using MessagePipe;
using Yaroyan.SproutWork.Application.CQRS.Command;
using Yaroyan.SproutWork.Application.CQRS.Event;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Command;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Event;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Query;
using System;

namespace Yaroyan.SproutWork.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] Environment _environment;

        protected override void Configure(IContainerBuilder builder)
        {

            // ================
            // QueryDB
            // ================
            builder.RegisterInstance<IQueryDBUOWProvider>(new QueryDBUOWProvider(_environment.DbConfig));

            // ================
            // CQRS - Command
            // ================
            builder.Register<ICommandBus, CommandsBus>(Lifetime.Singleton);

            // ================
            // CQRS - Query
            // ================

            // ================
            // CQRS - Event
            // ================
            builder.Register<IEventBus, EventBus>(Lifetime.Singleton);

            // ================
            // MessagePipe
            // ================
            MessagePipeOptions options = builder.RegisterMessagePipe();
        }
    }
}