using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace Yaroyan.SproutWork.Infrastructure.DataSource
{
    public abstract class AbstractSqliteConfig : ISqliteConfig
    {
        bool _isDisposed = false;
        private protected readonly DbConnectionStringBuilder builder;

        private protected AbstractSqliteConfig(DbConnectionStringBuilder builder)
        {
            this.builder = builder;
        }

        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            _isDisposed = !_isDisposed;
        }
        public string getConnectionString() => builder.ConnectionString;
    }
}
