using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Data;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository
{
    public abstract class GenericUnitOfWork : UnitOfWork, IGenericUnitOfWork
    {
        readonly ConcurrentDictionary<(Type, Type), IRepository> _repositories;
        public GenericUnitOfWork(string connectionString) : base(connectionString) { }

        public IRepository<T, U> GetRepository<T, U>()
            where T : IEntityId
            where U : IAggregateRoot<T>
        {
            return (IRepository<T, U>)_repositories.GetOrAdd((typeof(T), typeof(U)), CreateRepository<T, U>());
        }

        protected abstract IRepository CreateRepository<T, U>() where T : IEntityId where U : IAggregateRoot<T>;

        protected override void ResetRepositories() => _repositories.Clear();
    }
}

