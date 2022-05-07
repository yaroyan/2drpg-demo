using Microsoft.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg
{
    /// <summary>
    /// In-memory SQLite3データベースの設定クラス
    /// In-memory SQLite3 database configuration class
    /// </summary>
    public class InMemorySqliteConfig : ISqliteConfig
    {
        static readonly string s_dataSource = "inMemoryDB";
        readonly SqliteConnection _sqliteConnection;

        public InMemorySqliteConfig()
        {
            // 複数の接続間でインメモリDBを共有する
            // https://docs.microsoft.com/ja-jp/dotnet/standard/data/sqlite/in-memory-databases
            _sqliteConnection = new SqliteConnection(new SqliteConnectionStringBuilder { DataSource = s_dataSource, Mode = SqliteOpenMode.Memory, Cache = SqliteCacheMode.Shared }.ConnectionString);
            _sqliteConnection.Open();
        }

        /// <summary>
        /// 共有インメモリDBのビルダーを生成する。
        /// </summary>
        public SqliteConnectionStringBuilder createBuilder() => new SqliteConnectionStringBuilder { DataSource = s_dataSource };
    }
}
