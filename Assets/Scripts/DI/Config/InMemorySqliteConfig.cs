using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    /// <summary>
    /// In-memory SQLite3 database configuration class
    /// </summary>
    public class InMemorySqliteConfig : AbstractSqliteConfig, System.IDisposable
    {
        static readonly string s_masterDataBaseName = "master";

        bool _disposed = false;
        protected SqliteConnection connection;
        public InMemorySqliteConfig() : base(new SqliteConnectionStringBuilder { DataSource = s_masterDataBaseName, Mode = SqliteOpenMode.Memory, Cache = SqliteCacheMode.Shared })
        {
            // See link below.
            // https://docs.microsoft.com/ja-jp/dotnet/standard/data/sqlite/in-memory-databases
            connection = new SqliteConnection(getConnectionString());
            connection.Open();
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            connection.Dispose();
            _disposed = !_disposed;
        }
    }
}
