using Microsoft.Data.Sqlite;

namespace Com.Github.Yaroyan.Rpg
{
    public interface ISqliteConfig : IDbConfig
    {
        /// <summary>
        /// ビルダーを生成する。
        /// </summary>
        /// <returns></returns>
        SqliteConnectionStringBuilder createBuilder();
    }
}
