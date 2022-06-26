using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 身体的な技能を表現するクラス
/// </summary>
public class PhysicalSkill : AbstractSkill
{
    public PhysicalSkill(string name, int cost, IEnumerable<Effect> effects, string description) : base(name, cost, effects, description)
    {
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}
