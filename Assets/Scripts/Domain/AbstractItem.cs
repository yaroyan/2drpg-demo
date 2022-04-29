using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての道具類の抽象クラス
/// </summary>
public abstract class AbstractItem : IUseable, IDisposable
{
    [SerializeField] public int Code { get; private set; }
    [SerializeField] public int Name { get; private set; }
    [SerializeField] public int Price { get; private set; }
    [SerializeField] public string Description { get; private set; }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }
}
