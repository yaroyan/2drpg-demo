using System.Collections;
using System.Collections.Generic;
using System;

public interface IEntity<T> : IEquatable<IEntity<T>> where T : IEntityId
{
    T Id { get; }
}
