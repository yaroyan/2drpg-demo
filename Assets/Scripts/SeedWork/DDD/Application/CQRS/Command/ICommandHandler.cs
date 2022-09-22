using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SeedWork.DDD.Application.CQRS.Command
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        void Handle(T command);
    }
}