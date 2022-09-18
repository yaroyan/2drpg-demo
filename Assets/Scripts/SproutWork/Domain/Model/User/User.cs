using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.User
{
    public class User : Entity<UserId>, IAggregateRoot<UserId>
    {
        readonly UserId _id;
        public override UserId Id => _id;
        public Password Password { get; private set; }
        public User(UserId id, Password password)
        {
            _id = id;
            Password = password;
        }
    }
}
