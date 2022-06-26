namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        void Handle(T command);
    }
}