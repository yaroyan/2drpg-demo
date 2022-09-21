using VContainer;
using System.Collections.Generic;
using System.Linq;
using Yaroyan.SeedWork.DDD.Application.CQRS;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Application.CQRS.Handler;

namespace Yaroyan.SproutWork.Application.CQRS
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