using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Dapper;
using System.Linq;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.SqliteRepository.Scene
{
    public class SqliteSceneRepository : Repository<SceneId, Domain.Model.Scene.Scene>, ISceneRepository
    {
        public SqliteSceneRepository(IDbTransaction transaction) : base(transaction) { }

        public Domain.Model.Scene.Scene Find(SceneContext context)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Domain.Model.Scene.Scene> FindAll() =>
            Connection.Query<SceneId, (long BuildIndex, string SceneName), SceneId, Domain.Model.Scene.Scene>(
                $"select id, build_index as BuildIndex, name as SceneName, parent_id as parentId from scene",
                (id, context, parentId) => new Domain.Model.Scene.Scene(id, new SceneContext(Convert.ToInt32(context.BuildIndex), context.SceneName), parentId),
                Transaction,
                splitOn: "BuildIndex, parentId"
                );


        public override void Save(Domain.Model.Scene.Scene aggregateRoot)
        {
            Connection.Execute(
                $"insert into scene (id, build_index, name, parent_id) values (@Id, @BuildIndex, @Name, @ParentId)",
                new { Id = aggregateRoot.Id, BuildIndex = aggregateRoot.SceneContext.BuildIndex, Name = aggregateRoot.SceneContext.SceneName, ParentId = aggregateRoot.ParentId }
                );
        }

        public override void Update(Domain.Model.Scene.Scene aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}