using VContainer;
using VContainer.Unity;
using UnityEngine;
using MessagePipe;
using Com.Github.Yaroyan.Rpg.CQRS;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.Scene;

namespace Com.Github.Yaroyan.Rpg.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [UnityEngine.SerializeField] Environment _environment;

        protected override void Configure(IContainerBuilder builder)
        {

            // ================
            // Database
            // ================
            builder.Register<ISqliteConfig>(_ => _environment.DbConfig.IsInMemory ? new InMemorySqliteConfig() : new SqliteConfig(), Lifetime.Singleton);

            // ================
            // Repository
            // ================
            builder.Register<ISceneRepository, InMemorySceneRepository>(Lifetime.Singleton);

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