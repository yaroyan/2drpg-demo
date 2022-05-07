namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public interface ICommandBus
    {
        void Send<T>(T Command) where T : ICommand;
    }
}