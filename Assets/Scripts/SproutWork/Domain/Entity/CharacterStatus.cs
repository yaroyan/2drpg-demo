using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus
{
    [SerializeField] public int Level { get; set; }
    [SerializeField] public int Hp { get; set; }
    [SerializeField] public int Mp { get; set; }
    [SerializeField] public int Sp { get; set; }
    [SerializeField] public int Strength { get; set; }
    [SerializeField] public int Defense { get; set; }
    [SerializeField] public int Vitality { get; set; }
    [SerializeField] public int Intelligence { get; set; }
    [SerializeField] public int Mind { get; set; }
    [SerializeField] public int Dexterity { get; set; }
    [SerializeField] public int Agility { get; set; }
    [SerializeField] public int Luck { get; set; }
    [SerializeField] public StatusEffect StatusEffect { get; set; }
}
