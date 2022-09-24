using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Yaroyan.SproutWork.Domain.Model.User;
using Yaroyan.SproutWork.Infrastructure.DataSource.Repository.SqliteRepository.Scene;
using Yaroyan.SproutWork.Infrastructure.DataSource.Repository.SqliteRepository.User;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.SqliteRepository
{
    public class SqliteUnitOfWork : GenericUnitOfWork
    {
        public SqliteUnitOfWork(string connectionString) : base(connectionString) { }

        protected override IDbConnection CreateConnection(string connectionString) => new SqliteConnection(connectionString);

        protected override IRepository CreateRepository<T, U>()
        {
            var generics = (typeof(T), typeof(U));
            if (generics == (typeof(SceneId), typeof(Domain.Model.Scene.Scene)))
            {
                return new SqliteSceneRepository(_transaction);
            }
            else if (generics == (typeof(LocationId), typeof(Location)))
            {
                return new SqliteLocationRepository(_transaction);
            }
            else if (generics == (typeof(RouteId), typeof(Route)))
            {
                return new SqliteRouteRepository(_transaction);
            }
            else if (generics == (typeof(UserId), typeof(Domain.Model.User.User)))
            {
                return new SqliteUserRepository(_transaction);
            }
            else
            {
                throw new InvalidOperationException("Invalid generics repository.");
            }
        }
    }
}

