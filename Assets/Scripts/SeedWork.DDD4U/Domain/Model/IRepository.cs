using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD4U.Domain.Model
{
    public interface IRepository<T, U> where T : IEntityId where U : IAggregateRoot<T>
    {
        U Find(T entityId);
        IEnumerable<U> FindAll();
        void Delete(T entityId);
        void Save(U aggregateRoot);
        void Update(U aggregateRoot);
        T NextIdentity();
    }
}
