using System;

namespace Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}