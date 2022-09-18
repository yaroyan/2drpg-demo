using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.Game.RPG.Domain.Model.User
{
    public interface IUserRepository : IRepository<UserId, User> { }
}
