using System.Collections;
using System.Collections.Generic;
using System;

public class Identifier : IEquatable<Identifier>
{
    public Identifier(string id)
    {
        this.Value = id;
    }

    public string Value { get; }

    public bool Equals(Identifier other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Identifier)obj);
    }

    public override int GetHashCode()
    {
        return Value != null ? Value.GetHashCode() : 0;
    }
}
