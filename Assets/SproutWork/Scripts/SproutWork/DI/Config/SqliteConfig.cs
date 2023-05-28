using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Sqlite;

namespace Yaroyan.SproutWork.Infrastructure.DataSource
{
    /// <summary>
    /// SQLite3 database configuration class
    /// </summary>
    public class SqliteConfig : AbstractSqliteConfig
    {
        public SqliteConfig(string dbPath) : base(new SqliteConnectionStringBuilder { DataSource = dbPath }) { }
    }
}
