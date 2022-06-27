using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.DDD.SharedKernel;
using Yaroyan.Game.RPG.Domain.Model.Scene;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.SqliteRepository.Scene
{
    public class SqliteSceneRepository : Repository<SceneId, Domain.Model.Scene.Scene>, ISceneRepository
    {
        public SqliteSceneRepository(IDbTransaction transaction) : base(transaction) { }
        public override SceneId NextIdentity() => new SceneId(Guid.NewGuid().ToString("N"));

        public Domain.Model.Scene.Scene Find(SceneContext context)
        {
            throw new NotImplementedException();
        }

        public override void Save(Domain.Model.Scene.Scene aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public override void Update(Domain.Model.Scene.Scene aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}