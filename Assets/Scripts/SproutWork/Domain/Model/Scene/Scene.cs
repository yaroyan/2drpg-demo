using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// Scene Entity.
    /// </summary>
    public class Scene : AggregateRoot<SceneId>
    {
        public SceneId ParentId { get; private set; }
        SceneContext _sceneContext;
        public SceneContext SceneContext
        {
            get => _sceneContext;
            private set => _sceneContext = value ?? throw new ArgumentNullException(nameof(SceneContext));
        }
        public override SceneId Id { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public Scene(SceneId sceneId, SceneContext sceneContext, SceneId parentId) : base(Enumerable.Empty<IEvent>())
        {
            Id = sceneId;
            SceneContext = sceneContext;
            ParentId = parentId;
        }

        protected override void Mutate(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}