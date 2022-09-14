using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.Game.DDD.SharedKernel;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    /// <summary>
    /// An entity that represents a place, such as a building.
    /// </summary>
    public class Location : Entity<LocationId>, IAggregateRoot<LocationId>
    {
        readonly LocationId _id;
        public override LocationId Id { get => _id; }
        SceneId _sceneId;
        public SceneId SceneId
        {
            get => _sceneId;
            private set => _sceneId = value ?? throw new ArgumentNullException(nameof(SceneId));
        }
        string _name;
        public string Name { get => _name; private set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("must not be null or whitespace.", nameof(Name)); }
        public string Description { get; private set; }

        public Location(LocationId id, SceneId sceneId, string name, string description)
        {
            this._id = id;
            this.SceneId = sceneId;
            this.Name = name;
            this.Description = description;
        }

        public bool Equals(Location other)
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