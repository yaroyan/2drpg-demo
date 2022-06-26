using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptables/Create Environment")]
public class Environment : ScriptableObject
{
    [field: SerializeField] public DbConfig DbConfig { get; private set; }
}

[System.Serializable]
public class DbConfig
{
    [field: SerializeField] public bool IsInMemory { get; private set; }
}
