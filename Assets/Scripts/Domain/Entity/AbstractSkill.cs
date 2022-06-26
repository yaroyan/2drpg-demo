using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSkill : ISkill
{
    [SerializeField] public string Name { get; private set; }
    [SerializeField] public int Cost { get; private set; }
    [SerializeField] public IEnumerable<Effect> Effects { get; private set; }
    [SerializeField] public string Description { get; private set; }

    public AbstractSkill(string name, int cost, IEnumerable<Effect> effects, string description)
    {
        this.Name = name;
        this.Cost = cost;
        this.Effects = effects;
        this.Description = description;
    }

    public abstract void Execute();
}
