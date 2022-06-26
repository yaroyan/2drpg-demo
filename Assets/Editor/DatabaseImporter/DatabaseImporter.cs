using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Yaroyan.Game.EditorExtension.SubclassSelector;

namespace Yaroyan.Game.EditorExtension.Importer
{
    [CreateAssetMenu(menuName = "MyScriptables/Sqlite Importer")]
    public class DatabaseImporter : ScriptableObject
    {
        [field: SerializeReference, SubclassSelector] public IDbConnector Connector { get; private set; }
        [field: SerializeField] public Table Table { get; private set; }
    }

    public enum Table
    {
        [Alias("scenes")]
        Scenes
    }
}