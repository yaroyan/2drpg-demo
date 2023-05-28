using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Yaroyan.SproutWork.EditorExtension.SubclassSelector;
using Yaroyan.SeedWork.Common.Extension.Attribute;

namespace Yaroyan.SproutWork.EditorExtension.IXporter
{
    [CreateAssetMenu(menuName = "MyScriptables/Sqlite Importer")]
    public class DBIXporter : ScriptableObject
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