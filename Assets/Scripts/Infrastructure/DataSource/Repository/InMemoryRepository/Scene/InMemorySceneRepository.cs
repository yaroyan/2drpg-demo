using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.Game.RPG.Domain.Model.Scene;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource.Repository.InMemoryRepository.Scene
{
    public class InMemorySceneRepository : InMemoryRepository<SceneId, Domain.Model.Scene.Scene>, ISceneRepository
    {
        public Domain.Model.Scene.Scene Find(SceneContext context) => keyValuePairs.FirstOrDefault(e => e.Value.Equals(context)).Value;
        public override SceneId NextIdentity() => new SceneId(Guid.NewGuid());
    }
}
