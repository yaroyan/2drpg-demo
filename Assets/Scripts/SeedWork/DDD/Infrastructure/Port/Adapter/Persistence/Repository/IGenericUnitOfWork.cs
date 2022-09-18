using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository
{
    public interface IGenericUnitOfWork : IUnitOfWork
    {
        IRepository<T, IAggregateRoot<T>> GetRepository<T>() where T : IEntityId;
    }
}
