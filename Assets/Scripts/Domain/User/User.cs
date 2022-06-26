using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.User
{
    public class User : Entity<User>, IAggregateRoot<User>
    {
        readonly UserId _userId;
        public override IEntityId Id => _userId;
        public Password Password { get; private set; }
        public User(UserId userId, Password password)
        {
            _userId = userId;
            Password = password;
        }
    }
}
