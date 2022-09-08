using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    public abstract class AbstractSqliteConfig : ISqliteConfig
    {
        private protected readonly DbConnectionStringBuilder builder;

        private protected AbstractSqliteConfig(DbConnectionStringBuilder builder)
        {
            this.builder = builder;
        }
        public string getConnectionString() => builder.ConnectionString;
    }
}
