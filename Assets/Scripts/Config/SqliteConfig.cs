using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SqlKata.Execution;
using UnityEngine.AddressableAssets;
using SqlKata.Compilers;
using Microsoft.Data.Sqlite;

namespace Com.Github.Yaroyan.Rpg
{
    /// <summary>
    /// SQLite3データベースの設定クラス
    /// SQLite3 database configuration class
    /// </summary>
    public class SqliteConfig : ISqliteConfig
    {
        static readonly string s_dbAddress = "General/Database/database.sqlite3";
        static readonly string s_fileName = "database.sqlite3";
        readonly string _dataSource;

        public SqliteConfig(string dataPath)
        {
            this._dataSource = System.IO.Path.Combine(dataPath, s_fileName);
            CloneDB();
            SQLitePCL.Batteries_V2.Init();
        }

        /// <summary>
        /// DBをローカルにクローンする。
        /// </summary>
        void CloneDB()
        {
            // DBが既存の場合は処理を継続しない。
            if (System.IO.File.Exists(_dataSource)) return;
            Addressables.LoadAssetAsync<TextAsset>(s_dbAddress).Completed += db => System.IO.File.WriteAllBytes(_dataSource, db.Result.bytes);

            //string sourcePath = System.IO.Path.Combine(Application.streamingAssetsPath, s_dbAddress);
            //#if !UNITY_EDITOR && UNITY_ANDROID
            //        StartCoroutine(Copy(sourcePath, targetPath));
            //#else
            //            System.IO.File.Copy(sourcePath, targetPath);
            //#endif

            //            IEnumerator Copy(string sourcePath, string targetPath)
            //            {
            //                using (UnityWebRequest request = UnityWebRequest.Get(sourcePath))
            //                {
            //                    yield return request.SendWebRequest();
            //                    System.IO.File.WriteAllBytes(targetPath, request.downloadHandler.data);
            //                }
            //            }
        }

        public SqliteConnectionStringBuilder createBuilder() => new SqliteConnectionStringBuilder { DataSource = _dataSource };

    }
}
