using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.Game.DDD.SharedKernel
{
    public interface IRepository<T, U> where T : IEntityId where U : IAggregateRoot<U>
    {
        U Find(T entityId);
        U Find(U aggregateRoot);
        IEnumerable<U> FindAll();
        void Delete(T entityId);
        void Delete(U aggregateRoot);
        void Save(U aggregateRoot);
        T NextIdentity();
    }
}
