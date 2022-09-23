using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SproutWork.Domain.Model.User
{
    public class User : AggregateRoot<UserId>, IAggregateRoot<UserId>
    {
        public User(IEnumerable<IEvent> events) : base(events)
        {
        }

        readonly UserId _id;
        public override UserId Id { get => _id; }
        public Password Password { get; private set; }

        protected override void Mutate(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public User(UserId id, Password password) : base(Enumerable.Empty<IEvent>())
        {
            _id = id;
            Password = password;
        }
    }
}
