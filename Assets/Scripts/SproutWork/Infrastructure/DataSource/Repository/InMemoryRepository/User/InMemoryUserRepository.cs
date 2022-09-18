using System;
using System.Collections;
using Yaroyan.Game.RPG.Domain.Model.User;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.User
{
    public class InMemoryUserRepository : InMemoryRepository<UserId, Domain.Model.User.User>, IUserRepository
    {
        public override UserId NextIdentity() => new UserId(Guid.NewGuid());
    }
}