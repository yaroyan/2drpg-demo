using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Yaroyan.Game.DDD.SharedKernel;
using Yaroyan.Game.RPG.Domain.Model.Scene;
using Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.Scene;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.SqliteRepository
{
    public class SqliteUnitOfWork : UnitOfWork
    {
        public override ISceneRepository SceneRepository => _sceneRepository ??= new SqliteSceneRepository(_transaction);
        public override ILocationRepository LocationRepository => _locationRepository ??= new SqliteLocationRepository(_transaction);
        public override IRouteRepository RouteRepository => _routeRepository ??= new SqliteRouteRepository(_transaction);

        public SqliteUnitOfWork(string connectionString) : base(connectionString) { }

        protected override IDbConnection CreateConnection(string connectionString)
        {
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder
            {
                DataSource = connectionString
            };
            SQLitePCL.Batteries_V2.Init();
            return new SqliteConnection(builder.ConnectionString);
        }
    }
}

