using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    public abstract class BaseInMemorySqliteConfig : ISqliteConfig
    {
        static readonly string s_masterDataBaseName = "master";

        bool _disposed = false;
        protected SqliteConnection connection;

        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            connection.Dispose();
            _disposed = !_disposed;
        }

        public SqliteConnectionStringBuilder CreateBuilder() => new SqliteConnectionStringBuilder { DataSource = s_masterDataBaseName, Mode = SqliteOpenMode.Memory, Cache = SqliteCacheMode.Shared };
    }
}
