using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Yaroyan.SproutWork.Domain.Model.User;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository
{
    public class InMemoryUnitOfWork : IGenericUnitOfWork
    {
        readonly ConcurrentDictionary<(Type, Type), IRepository> _repositories;
        public void Commit() { }
        public void Rollback() { }
        public void Dispose() { }

        public IRepository<T, U> GetRepository<T, U>()
            where T : IEntityId
            where U : IAggregateRoot<T>
        {
            return (SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository.InMemoryRepository<T, U>)
                _repositories.GetOrAdd((typeof(T), typeof(U)), new SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository.InMemoryRepository<T, U>());
        }
    }
}
