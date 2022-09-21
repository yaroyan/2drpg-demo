using Yaroyan.SeedWork.DDD.Domain.Event;
namespace Yaroyan.SeedWork.DDD.Application.CQRS
{
    public interface ICommandBus
    {
        void Send<T>(T Command) where T : ICommand;
    }
}