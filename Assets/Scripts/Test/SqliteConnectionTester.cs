using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Sqlite;
using SqlKata.Execution;
using SqlKata.Compilers;
using UnityEngine.Networking;

/// <summary>
/// SQLite3の接続確認クラス
/// </summary>
public class SqliteConnectionTester : MonoBehaviour
{
    // DB名
    static readonly string s_dbName = "database.sqlite3";
    // テーブル名
    static readonly string s_tableName = "Scenes";
    // カラム名
    static readonly string s_columnName = "Id";

    void Start()
    {
        CloneDB();

        SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder
        {
            DataSource = System.IO.Path.Combine(GetPlatFormDataPath(), s_dbName)
        };

        SQLitePCL.Batteries_V2.Init();

        Debug.Log("接続確認");

        SqliteConnection connection = new SqliteConnection(builder.ConnectionString);

        // SQLiteのバージョン出力
        Debug.Log("バージョン情報：" + connection.ServerVersion);

        SqliteCompiler compiler = new SqliteCompiler();
        QueryFactory factory = new QueryFactory(connection, compiler);

        Debug.Log("レコード出力：" + s_tableName);
        // scenesテーブルからidカラムのデータを取得する。
        foreach (System.Object obj in factory.Query(s_tableName).Select(s_columnName).Get<System.Object>())
        {
            // テーブルからデータを取得できているか確認
            Debug.Log(obj.ToString());
        }
    }

    string GetPlatFormDataPath()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
        {
            return getFilesDir.Call<string>("getCanonicalPath");
        }
#else
        return Application.persistentDataPath;
#endif
    }

    void CloneDB()
    {
        string targetPath = System.IO.Path.Combine(GetPlatFormDataPath(), s_dbName);
        if (System.IO.File.Exists(targetPath)) return;
        string sourcePath = System.IO.Path.Combine(Application.streamingAssetsPath, s_dbName);

#if !UNITY_EDITOR && UNITY_ANDROID
        StartCoroutine(Copy(sourcePath, targetPath));
#else
        System.IO.File.Copy(sourcePath, targetPath);
#endif

#pragma warning disable CS8321 // ローカル関数は宣言されていますが、一度も使用されていません
        IEnumerator Copy(string sourcePath, string targetPath)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(sourcePath))
            {
                yield return request.SendWebRequest();
                System.IO.File.WriteAllBytes(targetPath, request.downloadHandler.data);
            }
        }
#pragma warning restore CS8321 // ローカル関数は宣言されていますが、一度も使用されていません
    }
}
