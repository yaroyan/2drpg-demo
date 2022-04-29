using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSkill
{
    [SerializeField] public string Name { get; private set; }
    [SerializeField] public int Cost { get; private set; }
    [SerializeField] public List<Effect> Effects { get; set; }
    /// <summary>
    /// 実行する。
    /// </summary>
    public abstract void Execute();
}
