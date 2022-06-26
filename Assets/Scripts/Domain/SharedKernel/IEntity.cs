using System.Collections;
using System.Collections.Generic;
using System;

public interface IEntity<T> : IEquatable<T>
{
    IEntityId Id { get; }
}
