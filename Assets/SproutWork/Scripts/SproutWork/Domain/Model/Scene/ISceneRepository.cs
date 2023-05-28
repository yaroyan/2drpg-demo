using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// SceneRepository Interface.
    /// </summary>
    public interface ISceneRepository : IRepository<SceneId, Scene>
    {
        /// <summary>
        /// Find Scene by SceneContext.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Scene Find(SceneContext context);
    }
}