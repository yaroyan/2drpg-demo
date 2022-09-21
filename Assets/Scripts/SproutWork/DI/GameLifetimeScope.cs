using VContainer;
using VContainer.Unity;
using UnityEngine;
using MessagePipe;
using Yaroyan.SproutWork.Application.CQRS;
using Yaroyan.SeedWork.DDD.Application.CQRS.Handler;
using Yaroyan.SeedWork.DDD.Application.CQRS;
using System;

namespace Com.Github.Yaroyan.Rpg.DI
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
            builder.Register<ICommandHandler<DeleteItemCommand>, DeleteItemCommandHandler>(Lifetime.Singleton);
            builder.Register<ICommandBus, CommandsBus>(Lifetime.Singleton);

            // ================
            // CQRS - Query
            // ================

            // ================
            // CQRS - Event
            // ================
            builder.Register<IEventHandler<ItemDeletedEvent>, ItemDeletedHandler>(Lifetime.Singleton);
            builder.Register<IEventBus, EventBus>(Lifetime.Singleton);

            // ================
            // MessagePipe
            // ================
            MessagePipeOptions options = builder.RegisterMessagePipe();
        }
    }
}