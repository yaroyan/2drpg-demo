using VContainer;
using System.Collections.Generic;
using System.Linq;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Application.CQRS.Command;
using Yaroyan.SeedWork.Common.VContainer;

namespace Yaroyan.SproutWork.Application.CQRS.Command
{
    public class CommandsBus : ICommandBus
    {
        readonly IObjectResolverContractor<ICommandHandler> _resolver;

        [Inject]
        public CommandsBus(IObjectResolverContractor<ICommandHandler> resolver)
        {
            this._resolver = resolver;
        }

        public void Send<T>(T command) where T : ICommand => this._resolver.Resolve<ICommandHandler<T>>().Handle(command);
    }
}