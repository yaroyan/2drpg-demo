using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;
using Yaroyan.Game.RPG.Domain.Model.Scene;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.Scene
{
    public class SqliteLocationRepository : Repository<LocationId, Location>, ILocationRepository
    {
        public SqliteLocationRepository(IDbTransaction transaction) : base(transaction) { }
        public override LocationId NextIdentity() => new LocationId(Guid.NewGuid().ToString("N"));

        public override void Save(Location aggregateRoot)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(Location aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}

