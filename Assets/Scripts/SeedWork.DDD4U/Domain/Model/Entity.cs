using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Domain.Model
{
    public abstract class Entity<T> : IEntity<T> where T : IEntityId
    {
        public abstract T Id { get; }

        public override bool Equals(object obj) => Equals(obj as Entity<T>);

        public bool Equals(IEntity<T> obj) => Equals(obj as Entity<T>);

        public bool Equals(Entity<T> other) => other is not null && EqualityComparer<T>.Default.Equals(Id, other.Id);

        public override int GetHashCode() => HashCode.Combine(Id);

        public static bool operator ==(Entity<T> left, Entity<T> right) => EqualityComparer<Entity<T>>.Default.Equals(left, right);

        public static bool operator !=(Entity<T> left, Entity<T> right) => !(left == right);
    }
}
