using VContainer;
using System.Collections.Generic;
using System.Linq;
namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public class CommandsBus : ICommandBus
    {
        readonly IObjectResolver container;

        [Inject]
        public CommandsBus(IObjectResolver container)
        {
            this.container = container;
        }

        public void Send<T>(T command) where T : ICommand => this.container.Resolve<ICommandHandler<T>>().Handle(command);
    }
}