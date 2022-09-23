using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using NUnit.Framework;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Command;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Query;

namespace Yaroyan.SeedWork.DDD4U.Test
{
    public class EventSourcingTest
    {
        [Test]
        private void Test()
        {
            var broker = new EventBroker();
            var character = new Character(broker);
            broker.Command(new ChangeAgeCommand(character, 123));
            int age = broker.Query<int>(new AgeQuery { Target = character });
            foreach (var b in broker.AllEvents) Console.WriteLine(b);
            broker.UndoLast();
            foreach (var b in broker.AllEvents) Console.WriteLine(b);
        }
    }

    public class CurrentState { }

    public class Character
    {
        public string Name { get; private set; }
        public CharacterId CharacterId { get; init; }
        List<IEvent> streams;
        public IEnumerable<IEvent> Streams { get => streams; }
        readonly CurrentState _currentState = new();

        int age;
        EventBroker broker;
        public Character(EventBroker broker)
        {
            this.broker = broker;
            broker.Commands += BrokerOnCommands;
            broker.Queries += BrokerOnQueries;
        }

        public void AddEvent(IEvent @event)
        {
            switch (@event)
            {
                case NameChangedEvent nameChangedEvent:
                    Apply(nameChangedEvent);
                    break;
            }
            streams.Add(@event);
        }

        void Apply(NameChangedEvent @event)
        {
            Name = @event.Name;
        }

        public Character(CharacterId id)
        {
            CharacterId = id;
        }

        void BrokerOnQueries(object sender, IQuery e)
        {
            switch (e)
            {
                case AgeQuery query when query.Target == this:
                    query.Result = age;
                    break;
            }
        }

        void BrokerOnCommands(object sender, ICommand e)
        {
            switch (e)
            {
                case ChangeAgeCommand command when command.Target == this && command.Register:
                    broker.AllEvents.Add(new AgeChangedEvent(this, age, command.Age));
                    age = command.Age;
                    break;
            }
        }
    }

    public record NameChangedEvent(string Name) { }

    public record CharacterId(string id) { }

    public class EventBroker
    {
        public IList<IEvent> AllEvents { get; private set; } = new List<IEvent>();
        public event EventHandler<ICommand> Commands;
        public event EventHandler<IQuery> Queries;

        public void Command(ICommand c)
        {
            Commands?.Invoke(this, c);
        }

        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T)q.Result;
        }

        public void UndoLast()
        {
            switch (AllEvents.LastOrDefault())
            {
                case AgeChangedEvent @event:
                    Command(new ChangeAgeCommand(@event.Target, @event.OldValue) { Register = false });
                    AllEvents.Remove(@event);
                    break;
            }
        }
    }
    public class Event : IEvent { }

    public class AgeChangedEvent : Event
    {
        public Character Target;
        public int OldValue, NewValue;
        public AgeChangedEvent(Character target, int oldValue, int newValue)
        {
            Target = target;
            OldValue = oldValue;
            NewValue = newValue;
        }
        public override string ToString()
        {
            return $"Age changed from {OldValue} to {NewValue}.";
        }
    }
    public class Query : IQuery
    {
        public object Result;
    }
    public class AgeQuery : Query
    {
        public Character Target;
    }
    public class Command : EventArgs, ICommand
    {
        public bool Register = true;
    }
    public class ChangeAgeCommand : Command
    {
        public Character Target;
        public int Age;
        public ChangeAgeCommand(Character target, int age)
        {
            this.Target = target;
            this.Age = age;
        }
    }
}