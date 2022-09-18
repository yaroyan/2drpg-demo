using Microsoft.Data.Sqlite;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    public interface ISqliteConfig : IDbConfig
    {
        /// <summary>
        /// Provide connection string.
        /// </summary>
        /// <returns></returns>
        string getConnectionString();
    }
}
