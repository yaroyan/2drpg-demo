using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    /// <summary> 健康 </summary>
    None = 0,
    /// <summary> 毒 </summary>
    Poisoned = 1 << 0,
    /// <summary> 盲目 </summary>
    Blind = 1 << 2,
    /// <summary> 混乱 </summary>
    Confused = 1 << 3,
    /// <summary> 気絶 </summary>
    Stunned = 1 << 4,
    /// <summary> 麻痺 </summary>
    Paralyzed = 1 << 5,
    /// <summary> 魅了 </summary>
    Charmed = 1 << 6,
    /// <summary> 沈黙 </summary>
    Silenced = 1 << 7,
    /// <summary> 流血 </summary>
    Bleeding = 1 << 8,
    /// <summary> 石化 </summary>
    Stoned = 1 << 9,
    /// <summary> 凍傷 </summary>
    Frozen = 1 << 10,
    /// <summary> 火傷 </summary>
    Framed = 1 << 11,
    /// <summary> 空腹 </summary>
    Hungry = 1 << 12,
    /// <summary> 口渇 </summary>
    Thirsty = 1 << 13,
    /// <summary> 泥酔 </summary>
    Drunk = 1 << 14,
    /// <summary> 死亡 </summary>
    Dead = 1 << 15,
    /// <summary> 呪い </summary>
    Cursed = 1 << 16,
    /// <summary> 鈍足 </summary>
    Slowed = 1 << 17,
    /// <summary> 恐怖 </summary>
    Frightened = 1 << 18,
    /// <summary> 狂暴化 </summary>
    Berserk = 1 << 19,
    /// <summary> 拘束 </summary>
    Bound = 1 << 20,
    /// <summary> 挑発（激昂） </summary>
    Taunted = 1 << 21,
}
