using Yaroyan.SeedWork.DDD.Domain.Event;
namespace Yaroyan.SeedWork.DDD.Application.CQRS.Command
{
    public interface ICommandBus
    {
        void Send<T>(T Command) where T : ICommand;
    }
}