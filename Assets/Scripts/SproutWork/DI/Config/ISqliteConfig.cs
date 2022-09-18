using System;

namespace Yaroyan.SproutWork.Infrastructure.DataSource
{
    public interface ISqliteConfig : IDbConfig, IDisposable
    {
        /// <summary>
        /// Provide connection string.
        /// </summary>
        /// <returns></returns>
        string getConnectionString();
    }
}
