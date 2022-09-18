using Microsoft.Data.Sqlite;
using System.Data.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SproutWork.Infrastructure.DataSource
{
    /// <summary>
    /// In-memory SQLite3 database configuration class
    /// </summary>
    public class InMemorySqliteConfig : AbstractSqliteConfig
    {
        static readonly string s_masterDataBaseName = "QueryDB";

        bool _isDisposed = false;
        SqliteConnection _connection;

        public InMemorySqliteConfig() : base(new SqliteConnectionStringBuilder { DataSource = s_masterDataBaseName, Mode = SqliteOpenMode.Memory, Cache = SqliteCacheMode.Shared })
        {
            // See link below.
            // https://docs.microsoft.com/ja-jp/dotnet/standard/data/sqlite/in-memory-databases
            _connection = new SqliteConnection(getConnectionString());
            _connection.Open();
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing) _connection.Dispose();
            _isDisposed = !_isDisposed;
        }
    }
}
