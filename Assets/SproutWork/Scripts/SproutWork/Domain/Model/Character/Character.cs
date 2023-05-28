using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : IEquatable<Character>
{
    readonly Identifier id;
    CharacterName characterName;

    public Character(Identifier id, CharacterName name)
    {
        this.id = id;
        this.characterName = name;
    }

    public Character(CharacterName name)
    {
        this.id = new Identifier(Guid.NewGuid().ToString());
        this.characterName = name;
    }

    public bool Equals(Character other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(id, other.id);
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Character)obj);
    }

    public override int GetHashCode()
    {
        return (id != null ? id.GetHashCode() : 0);
    }
}
