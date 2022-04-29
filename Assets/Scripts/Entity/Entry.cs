using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Github.Yaroyan.Constant;

namespace Com.Github.Yaroyan
{
    /// <summary>
    /// Sceneの遷移地点クラス
    /// </summary>
    public class Entry : ScriptableObject
    {
        [SerializeField] public int Id { get; private set; }
        [SerializeField] public string Name { get; private set; }
        [SerializeField] public Scene Scene { get; private set; }
        [SerializeField] public string Description { get; private set; }
        [SerializeField] public EntryMasterTypes entryMasterTypes { get; set; }
    }

}
