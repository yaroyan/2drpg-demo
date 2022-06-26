using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    [SerializeField] public int Id { get; private set; }
    [SerializeField] public float DamageRatio { get; private set; }
    [SerializeField] public Element Element { get; private set; }

    public Effect(int id, float damageRatio, Element element)
    {
        this.Id = id;
        this.DamageRatio = damageRatio;
        this.Element = element;
    }
}
