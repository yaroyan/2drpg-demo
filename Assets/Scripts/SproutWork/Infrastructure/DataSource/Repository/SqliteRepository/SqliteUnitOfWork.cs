using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Domain.Model.User;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.Scene;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.User;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository
{
    public class SqliteUnitOfWork : UnitOfWork, IUnitOfWork
    {
        ISceneRepository _sceneRepository;
        ILocationRepository _locationRepository;
        IRouteRepository _routeRepository;
        IUserRepository _userRepository;

        public ISceneRepository SceneRepository => _sceneRepository ??= new SqliteSceneRepository(_transaction);
        public ILocationRepository LocationRepository => _locationRepository ??= new SqliteLocationRepository(_transaction);
        public IRouteRepository RouteRepository => _routeRepository ??= new SqliteRouteRepository(_transaction);
        public IUserRepository UserRepository => _userRepository ??= new SqliteUserRepository(_transaction);

        public SqliteUnitOfWork(string connectionString) : base(connectionString) { }

        protected override IDbConnection CreateConnection(string connectionString) => new SqliteConnection(connectionString);

        protected override void ResetRepositories()
        {
            _sceneRepository = null;
            _locationRepository = null;
            _routeRepository = null;
            _userRepository = null;
        }
    }
}

