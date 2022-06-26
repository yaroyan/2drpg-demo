using Microsoft.Data.Sqlite;

namespace Com.Github.Yaroyan.Rpg
{
    public interface ISqliteConfig : IDbConfig
    {
        /// <summary>
        /// �r���_�[�𐶐�����B
        /// </summary>
        /// <returns></returns>
        SqliteConnectionStringBuilder CreateBuilder();
    }
}
