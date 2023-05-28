using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マネージドなupdateを提供するインターフェース。
/// Unity標準のupdateのオーバーヘッドを軽減する。
/// </summary>
public interface IUpdatable
{
    void ManagedUpdate();
}
