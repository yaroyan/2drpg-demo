using VContainer;
using VContainer.Unity;
using UnityEngine;
using MessagePipe;
using Yaroyan.SproutWork.Application.CQRS.Command;
using Yaroyan.SproutWork.Application.CQRS.Event;
using Yaroyan.SproutWork.Application.CQRS.Query;
using Yaroyan.SeedWork.DDD.Application.CQRS.Command;
using Yaroyan.SeedWork.DDD.Application.CQRS.Event;
using Yaroyan.SeedWork.DDD.Application.CQRS.Query;
using Yaroyan.SproutWork.Domain.Event.Other;
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