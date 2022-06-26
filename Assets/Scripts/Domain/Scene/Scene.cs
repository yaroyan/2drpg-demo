using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    /// <summary>
    /// Scene Entity.
    /// </summary>
    public class Scene : IAggregateRoot<Scene>
    {
        readonly SceneId _id;
        public IEntityId Id { get => _id; }
        public SceneId ParentId { get; private set; }
        SceneContext _sceneContext;
        public SceneContext SceneContext
        {
            get => _sceneContext;
            private set => _sceneContext = value ?? throw new ArgumentNullException(nameof(SceneContext));
        }

        public Scene(SceneId sceneId, SceneContext sceneContext, SceneId parentId)
        {
            _id = sceneId;
            SceneContext = sceneContext;
            ParentId = parentId;
        }

        public bool Equals(Scene other)
        {
            throw new NotImplementedException();
        }
    }
}

/**
 * Delete
 * Scene&Location&Route
 */