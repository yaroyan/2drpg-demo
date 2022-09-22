using System;
using System.Collections;
using Yaroyan.SproutWork.Domain.Model.User;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository.User
{
    public class InMemoryUserRepository : InMemoryRepository<UserId, Domain.Model.User.User>, IUserRepository
    {
        public override UserId NextIdentity() => new UserId(Guid.NewGuid());
    }
}