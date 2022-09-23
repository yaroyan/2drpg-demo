using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    /// <summary>
    /// An entity that represents a place, such as a building.
    /// </summary>
    public class Location : AggregateRoot<LocationId>, IAggregateRoot<LocationId>
    {
        public override LocationId Id { get; init; }
        SceneId _sceneId;
        public SceneId SceneId
        {
            get => _sceneId;
            private set => _sceneId = value ?? throw new ArgumentNullException(nameof(SceneId));
        }
        string _name;
        public string Name { get => _name; private set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("must not be null or whitespace.", nameof(Name)); }
        public string Description { get; private set; }

        public Location(LocationId id, SceneId sceneId, string name, string description) : base(Enumerable.Empty<IEvent>())
        {
            this.Id = id;
            this.SceneId = sceneId;
            this.Name = name;
            this.Description = description;
        }

        public Location(IEnumerable<IEvent> events) : base(events) { }

        public bool Equals(Location other)
        {
            throw new NotImplementedException();
        }

        protected override void Mutate(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
//public enum World { }
//public enum Continent { }
//public enum Region { }
//public enum Dungeon { }
//public enum Country { }
//public enum City { }
//public enum District { }
//public enum Building { }