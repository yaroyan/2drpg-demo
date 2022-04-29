using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    [SerializeField] public int Id { get; set; }
    [SerializeField] public int FixedDamage { get; set; }
    [SerializeField] public float DamageRatio { get; set; }
    [SerializeField] public Element Element { get; set; }
}
