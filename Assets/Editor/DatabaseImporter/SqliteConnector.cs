using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Yaroyan.Game.EditorExtension.Importer
{
    [System.Serializable]
    public class SqliteConnector : IDbConnector
    {
        [field: SerializeField] public TextAsset Asset { get; private set; }
        public string GetAssetRelativePath() => AssetDatabase.GetAssetPath(Asset);
        public string GetAssetAbsolutePath() => System.IO.Path.GetFullPath(GetAssetRelativePath());
        public string GetConnectionString() => new SqliteConnectionStringBuilder { DataSource = GetAssetAbsolutePath()}.ConnectionString;
        public IDbConnection ProvideConnection() => new SqliteConnection(GetConnectionString());
    }
}
