using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository
{
    public abstract class InMemoryRepository<T, U> : IRepository<T, U> where T : IEntityId where U : IAggregateRoot<T>
    {
        protected ConcurrentDictionary<T, U> keyValuePairs = new();
        public U Find(T id) => keyValuePairs.GetValueOrDefault(id);
        public IEnumerable<U> FindAll() => keyValuePairs.Values;
        public void Delete(T id) => keyValuePairs.Remove(id, out _);
        public void Save(U entity) => keyValuePairs[entity.Id] = entity;
        public void Update(U entity) => Save(entity);
        public abstract T NextIdentity();
    }
}