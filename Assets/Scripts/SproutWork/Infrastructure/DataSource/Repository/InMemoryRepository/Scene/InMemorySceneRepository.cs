using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using Yaroyan.SeedWork.DDD.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;

namespace Yaroyan.SproutWork.Infrastructure.DataSource.Repository.InMemoryRepository.Scene
{
    public class InMemorySceneRepository : InMemoryRepository<SceneId, Domain.Model.Scene.Scene>, ISceneRepository
    {
        public Domain.Model.Scene.Scene Find(SceneContext context) => keyValuePairs.FirstOrDefault(e => e.Value.Equals(context)).Value;
        public override SceneId NextIdentity() => new SceneId(Guid.NewGuid());
    }
}
