using Dapper;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository
{
    public abstract class Repository<T, U> : IRepository<T, U> where T : IEntityId where U : IAggregateRoot<T>
    {
        static readonly ConcurrentDictionary<Type, Func<string, T>> s_cachedInstantiationExpression = new();
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection => Transaction.Connection;
        protected virtual string TableName => nameof(U);

        public Repository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public virtual U Find(T entityId) => Connection.QueryFirstOrDefault<U>($"select * from {TableName} where id = @id", new { id = entityId }, Transaction);
        public virtual U Find(U aggregateRoot) => Connection.QueryFirstOrDefault<U>($"select * from {TableName} where id = @id", new { id = aggregateRoot.Id }, Transaction);
        public virtual IEnumerable<U> FindAll() => Connection.Query<U>($"select * from {TableName}", Transaction).ToList();
        public virtual void Delete(T entityId) => Connection.Query<U>($"delete from {TableName} where id = @id", param: new { id = entityId }, Transaction);
        public virtual void Delete(U aggregateRoot) => Connection.Query<U>($"delete from {TableName} where id = @id", param: new { id = aggregateRoot.Id }, Transaction);
        public abstract void Save(U aggregateRoot);
        public abstract void Update(U aggregateRoot);
        public T NextIdentity()
        {
            var expression = s_cachedInstantiationExpression.GetOrAdd(
                            typeof(T),
                            Expression.Lambda<Func<string, T>>(Expression.New(typeof(T).GetConstructor(new[] { typeof(string) }), Expression.Parameter(typeof(string), "Id"))).Compile());
            return expression(Guid.NewGuid().ToString());
        }
    }
}
