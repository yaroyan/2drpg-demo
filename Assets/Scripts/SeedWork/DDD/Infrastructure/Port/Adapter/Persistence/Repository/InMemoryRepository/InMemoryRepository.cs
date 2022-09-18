using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using Yaroyan.SeedWork.Common.Extension;

namespace Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository
{
    public abstract class InMemoryRepository<T, U> : IRepository<T, U> where T : IEntityId where U : IAggregateRoot<T>
    {
        protected Dictionary<T, U> keyValuePairs = new Dictionary<T, U>();
        public U Find(T id) => keyValuePairs.GetValueOrDefault(id);
        public U Find(U entity) => keyValuePairs.GetValueOrDefault(entity.Id);
        public IEnumerable<U> FindAll() => keyValuePairs.Values;
        public void Delete(U entity) => keyValuePairs.Remove(entity.Id);
        public void Delete(T id) => keyValuePairs.Remove(id);
        public void Save(U entity) => keyValuePairs[entity.Id] = entity;
        public void Update(U entity) => Save(entity);
        public abstract T NextIdentity();
    }
}