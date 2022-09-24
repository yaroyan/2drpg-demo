using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository
{
    public sealed class InMemoryRepository<T, U> : IRepository<T, U> where T : IEntityId where U : IAggregateRoot<T>
    {
        static readonly ConcurrentDictionary<Type, Func<string, T>> s_cachedInstantiationExpression = new();
        readonly ConcurrentDictionary<T, U> keyValuePairs = new();
        public U Find(T id) => keyValuePairs.GetValueOrDefault(id);
        public IEnumerable<U> FindAll() => keyValuePairs.Values;
        public void Delete(T id) => keyValuePairs.Remove(id, out _);
        public void Save(U entity) => keyValuePairs[entity.Id] = entity;
        public void Update(U entity) => Save(entity);
        /// <summary>
        /// If performance issues are a concern, implement it appropriately in a subclass.
        /// </summary>
        /// <returns></returns>
        public T NextIdentity() => s_cachedInstantiationExpression.GetOrAdd(typeof(T), CompileExpression())(Guid.NewGuid().ToString());

        Func<string, T> CompileExpression()
        {
            var ctor = typeof(T).GetConstructor(new[] { typeof(string) });
            var param = Expression.Parameter(typeof(string), "Id");
            var body = Expression.New(ctor, param);
            return Expression.Lambda<Func<string, T>>(body, param).Compile();
        }
    }
}