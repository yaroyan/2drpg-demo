using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.SeedWork.DDD.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// Scene Entity.
    /// </summary>
    public class Scene : IAggregateRoot<SceneId>
    {
        readonly SceneId _id;
        public SceneId Id
        {
            get => _id;
            init => _id = value ?? throw new ArgumentNullException(nameof(Id));
        }
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

        public bool Equals(IEntity<SceneId> other)
        {
            throw new NotImplementedException();
        }
    }
}