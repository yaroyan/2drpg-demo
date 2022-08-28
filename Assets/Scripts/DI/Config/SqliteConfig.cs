using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Yaroyan.Game.RPG.Helper;


namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    /// <summary>
    /// SQLite3 database configuration class
    /// </summary>
    public class SqliteConfig : BaseInMemorySqliteConfig
    {
        static readonly string s_masterDatabasePath = "Database/SQLite/Master/master.sqlite3";
        static readonly string s_dataSource;

        static SqliteConfig()
        {
            s_dataSource = System.IO.Path.Combine(UnityPathHelper.GetPlatformIndependentStreamingAssetsPath(), s_masterDatabasePath);
        }

        public SqliteConfig()
        {
            connection = new SqliteConnection(CreateBuilder().ConnectionString);
            connection.Open();
            using (var sourceConnection = new SqliteConnection(new SqliteConnectionStringBuilder { DataSource = s_dataSource }.ConnectionString))
            {
                sourceConnection.Open();
                sourceConnection.BackupDatabase(connection);
            }
        }

        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        //static void initialize()
        //{
        //    // If the target file exists locally, processing does not continue.
        //    if (System.IO.File.Exists(s_dataSource)) return;
        //    // Synchronously load and copy files locally.
        //    var handle = Addressables.LoadAssetAsync<TextAsset>(s_dbAddress);
        //    System.IO.File.WriteAllBytes(s_dataSource, handle.WaitForCompletion().bytes);
        //    Addressables.Release(handle);
        //}
    }
}
