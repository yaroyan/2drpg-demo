using VContainer;
using VContainer.Unity;
using Com.Github.Yaroyan.Rpg.CQRS;

namespace Com.Github.Yaroyan.Rpg.DI
{
    /// <summary>
    /// DI Contaienr for CQRS - Command
    /// </summary>
    public class CommandLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<DeleteItemCommandHandler>(Lifetime.Singleton).As<ICommandHandler<DeleteItemCommand>>();
            builder.Register<CommandsBus>(Lifetime.Singleton).As<ICommandBus>();
        }
    }
}
