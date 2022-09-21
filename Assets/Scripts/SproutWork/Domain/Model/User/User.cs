using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Yaroyan.SeedWork.DDD.Domain.Model;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SproutWork.Domain.Model.User
{
    public class User : Entity<UserId>, IAggregateRoot<UserId>
    {
        public User(IEnumerable<IEvent> events) : base(events)
        {
        }

        public override UserId Id { get; init; }
        public Password Password { get; private set; }

        protected override void Mutate(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public User(UserId id, Password password) : base(Enumerable.Empty<IEvent>())
        {
            Id = id;
            Password = password;
        }
    }
}
