using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Application.CQRS.Command
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        void Handle(T command);
    }
}