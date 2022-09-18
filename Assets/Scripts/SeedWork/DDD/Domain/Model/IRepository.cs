using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Domain.Model
{
    public interface IRepository<T, U> where T : IEntityId where U : IAggregateRoot<T>
    {
        U Find(T entityId);
        U Find(U aggregateRoot);
        IEnumerable<U> FindAll();
        void Delete(T entityId);
        void Delete(U aggregateRoot);
        void Save(U aggregateRoot);
        void Update(U aggregateRoot);
        T NextIdentity();
    }
}