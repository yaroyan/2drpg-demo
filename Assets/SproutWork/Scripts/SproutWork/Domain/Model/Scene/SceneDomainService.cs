using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// Domain service for Scene entity.
    /// </summary>
    public class SceneDomainService
    {
        readonly ISceneRepository _sceneRepository;

        public SceneDomainService(ISceneRepository sceneRepository)
        {
            this._sceneRepository = sceneRepository;
        }

        /// <summary>
        /// Determine duplicate Scenes entities.
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public bool isDuplicated(Scene scene) => _sceneRepository.Find(scene.SceneContext) is not null;
    }
}