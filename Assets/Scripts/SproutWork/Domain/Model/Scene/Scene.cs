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
        readonly SceneId _id;
        public override SceneId Id { get => _id; }

        public Scene(SceneId sceneId, SceneContext sceneContext, SceneId parentId) : base(Enumerable.Empty<IEvent>())
        {
            _id = sceneId;
            SceneContext = sceneContext;
            ParentId = parentId;
        }

        protected override void Mutate(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}