using VContainer;
using VContainer.Unity;
using UnityEngine;
using MessagePipe;
using Com.Github.Yaroyan.Rpg.CQRS;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.Scene;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository;
using Yaroyan.Game.RPG.Infrastructure.DataSource;
using System;

namespace Com.Github.Yaroyan.Rpg.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [UnityEngine.SerializeField] Environment _environment;

        protected override void Configure(IContainerBuilder builder)
        {

            // ================
            // QueryDB
            // ================
            builder.RegisterFactory<IUnitOfWork>(() => new SqliteUnitOfWork(_environment.DbConfig.GetSqliteQueryDBConfig().getConnectionString()));

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