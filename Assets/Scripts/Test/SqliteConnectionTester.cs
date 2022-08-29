using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SqlKata.Compilers;
using SqlKata.Execution;
using Microsoft.Data.Sqlite;
using UnityEngine.Networking;
using Yaroyan.Game.RPG.Infrastructure.DataSource;

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

        SQLitePCL.Batteries_V2.Init();

        Debug.Log("接続確認");

        SqliteConfig config = new SqliteConfig();

        Debug.Log(config.CreateBuilder().ConnectionString);
        SqliteConnection connection = new SqliteConnection(config.CreateBuilder().ConnectionString);

        // SQLiteのバージョン出力
        Debug.Log("バージョン情報：" + connection.ServerVersion);

        SqliteCompiler compiler = new SqliteCompiler();
        QueryFactory factory = new QueryFactory(connection, compiler);

        Debug.Log("レコード出力：" + s_tableName);

        // scenesテーブルからidカラムのデータを取得する。
        foreach (System.Object obj in factory.Query(s_tableName.ToLower()).Select(s_columnName).Get<System.Object>())
        {
            // テーブルからデータを取得できているか確認
            Debug.Log(obj.ToString());
        }
    }
}
