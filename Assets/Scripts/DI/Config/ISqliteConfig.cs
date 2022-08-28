using Microsoft.Data.Sqlite;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    public interface ISqliteConfig : IDbConfig, System.IDisposable
    {
        /// <summary>
        /// �r���_�[�𐶐�����B
        /// </summary>
        /// <returns></returns>
        SqliteConnectionStringBuilder CreateBuilder();
    }
}
