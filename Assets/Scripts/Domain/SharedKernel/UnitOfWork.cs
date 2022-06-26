using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.RPG.Domain.Model.Scene;

namespace Yaroyan.Game.DDD.SharedKernel
{

    public abstract class UnitOfWork : IUnitOfWork
    {
        protected ISceneRepository _sceneRepository;
        protected ILocationRepository _locationRepository;
        protected IRouteRepository _routeRepository;

        IDbConnection _connection;
        protected IDbTransaction _transaction;

        bool _isDisposed;

        public abstract ISceneRepository SceneRepository { get; }
        public abstract ILocationRepository LocationRepository { get; }
        public abstract IRouteRepository RouteRepository { get; }

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

        void ResetRepositories()
        {
            _sceneRepository = null;
            _locationRepository = null;
            _routeRepository = null;
        }

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