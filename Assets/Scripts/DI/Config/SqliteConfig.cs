using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Microsoft.Data.Sqlite;

namespace Com.Github.Yaroyan.Rpg
{
    /// <summary>
    /// SQLite3 database configuration class
    /// </summary>
    public class SqliteConfig : ISqliteConfig
    {
        static readonly string s_dbAddress = "General/Database/database";
        static readonly string s_fileName = "database.sqlite3";
        static readonly string s_dataSource;

        static SqliteConfig()
        {
            s_dataSource = System.IO.Path.Combine(GetPlatFormDataPath(), s_fileName);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            // ローカルに存在する場合は処理を継続しない。
            if (System.IO.File.Exists(s_dataSource)) return;
            SQLitePCL.Batteries_V2.Init();
            // 同期ロードしてローカルにコピーする。
            var handle = Addressables.LoadAssetAsync<TextAsset>(s_dbAddress);
            System.IO.File.WriteAllBytes(s_dataSource, handle.WaitForCompletion().bytes);
            Addressables.Release(handle);
        }

        static string GetPlatFormDataPath()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
        {
            return getFilesDir.Call<string>("getCanonicalPath");
        }
#else
            return UnityEngine.Application.persistentDataPath;
#endif
        }

        public SqliteConnectionStringBuilder CreateBuilder() => new SqliteConnectionStringBuilder { DataSource = s_dataSource };

    }
}
