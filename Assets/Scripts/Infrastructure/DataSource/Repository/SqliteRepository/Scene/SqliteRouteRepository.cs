using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;
using Yaroyan.Game.RPG.Domain.Model.Scene;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.Scene
{
    public class SqliteRouteRepository : Repository<RouteId, Route>, IRouteRepository
    {
        public SqliteRouteRepository(IDbTransaction transaction) : base(transaction) { }
        public override RouteId NextIdentity() => new RouteId(Guid.NewGuid().ToString("N"));

        public override void Save(Route aggregateRoot)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(Route aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}
