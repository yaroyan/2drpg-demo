using Yaroyan.SeedWork.DDD4U.Domain.Event;
namespace Yaroyan.SeedWork.DDD4U.Application.CQRS.Command
{
    public interface ICommandBus
    {
        void Send<T>(T Command) where T : ICommand;
    }
}