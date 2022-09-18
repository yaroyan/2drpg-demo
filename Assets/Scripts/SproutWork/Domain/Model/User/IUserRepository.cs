using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.User
{
    public interface IUserRepository : IRepository<UserId, User> { }
}
