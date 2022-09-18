using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository
{
    public abstract class Repository<T, U> : IRepository<T, U> where T : IEntityId where U : IAggregateRoot<T>
    {
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
        public abstract T NextIdentity();
    }
}
