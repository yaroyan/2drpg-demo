using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.User
{
    public interface IUserRepository : IRepository<UserId, User> { }
}
