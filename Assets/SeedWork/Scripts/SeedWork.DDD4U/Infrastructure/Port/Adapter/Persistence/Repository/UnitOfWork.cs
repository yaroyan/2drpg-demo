using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository
{

    public abstract class UnitOfWork : IUnitOfWork
    {
        IDbConnection _connection;
        protected IDbTransaction _transaction;
        bool _isDisposed;

        public UnitOfWork(string connectionString)
        {
            _connection = CreateConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        protected abstract IDbConnection CreateConnection(string connectionString);

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        protected abstract void ResetRepositories();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (!ReferenceEquals(_transaction, null))
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (!ReferenceEquals(_connection, null))
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _isDisposed = !_isDisposed;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}