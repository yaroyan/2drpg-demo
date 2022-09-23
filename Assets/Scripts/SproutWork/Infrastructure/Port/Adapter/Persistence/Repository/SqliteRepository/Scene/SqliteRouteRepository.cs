using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SproutWork.Domain.Model.Scene;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.SqliteRepository.Scene
{
    public class SqliteRouteRepository : Repository<RouteId, Route>, IRouteRepository
    {
        public SqliteRouteRepository(IDbTransaction transaction) : base(transaction) { }
        public override RouteId NextIdentity() => new RouteId(Guid.NewGuid());

        public override void Save(Route aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public override void Update(Route aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}
