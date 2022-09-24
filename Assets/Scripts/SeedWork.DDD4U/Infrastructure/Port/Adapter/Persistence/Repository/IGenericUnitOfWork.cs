using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository
{
    public interface IGenericUnitOfWork : IUnitOfWork
    {
        IRepository<T, U> GetRepository<T, U>() where T : IEntityId where U : IAggregateRoot<T>;
    }
}
