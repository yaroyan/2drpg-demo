using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Microsoft.Data.Sqlite;
using System.Linq;
using Yaroyan.Game.RPG.Helper;


namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    /// <summary>
    /// SQLite3 database configuration class
    /// </summary>
    public class SqliteConfig : AbstractSqliteConfig
    {
        static readonly string s_queryDBPath;
        static readonly string s_addressablesAddress = "Assets/Addressables/Database/SQLite/QueryDB/query.bytes";

        static SqliteConfig()
        {
            s_queryDBPath = System.IO.Path.Combine(UnityPathHelper.GetPlatformIndependentDataPath(), s_addressablesAddress.Split("/").Last());
        }

        public SqliteConfig() : base(new SqliteConnectionStringBuilder { DataSource = s_queryDBPath }) { }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void initialize()
        {
            // If the target file exists locally, processing does not continue.
            // Synchronously load and copy files locally.
            var handle = Addressables.LoadAssetAsync<TextAsset>(s_addressablesAddress);
            System.IO.File.WriteAllBytes(s_queryDBPath, handle.WaitForCompletion().bytes);
            Addressables.Release(handle);
        }
    }
}
