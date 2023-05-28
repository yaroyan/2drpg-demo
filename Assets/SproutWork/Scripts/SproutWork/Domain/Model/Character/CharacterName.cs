using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class CharacterName : IEquatable<CharacterName>
{
    readonly string firstName;
    readonly string lastName;
    readonly string[] middleNames;

    public CharacterName(string firstName, string lastName, params string[] middleNames)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.middleNames = middleNames;
    }

    public string FirstName { get => this.firstName; }
    public string Lastname { get => this.lastName; }
    public string[] MiddleNames { get => this.middleNames; }
    public string GetFullName(string delimiter) => string.Join(delimiter, firstName, string.Join(delimiter, middleNames), lastName);

    public bool Equals(CharacterName other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(firstName, other.firstName, StringComparison.Ordinal)
            && string.Equals(lastName, other.lastName, StringComparison.Ordinal)
            && Enumerable.SequenceEqual(middleNames, other.middleNames);
    }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CharacterName)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(firstName, lastName, middleNames);
    }

}
