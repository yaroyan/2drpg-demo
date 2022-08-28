using Microsoft.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.Game.RPG.Infrastructure.DataSource
{
    /// <summary>
    /// In-memory SQLite3�f�[�^�x�[�X�̐ݒ�N���X
    /// In-memory SQLite3 database configuration class
    /// </summary>
    public class InMemorySqliteConfig : BaseInMemorySqliteConfig
    {
        public InMemorySqliteConfig()
        {
            // �����̐ڑ��ԂŃC��������DB�����L����
            // https://docs.microsoft.com/ja-jp/dotnet/standard/data/sqlite/in-memory-databases
            connection = new SqliteConnection(CreateBuilder().ConnectionString);
            connection.Open();
        }
    }
}
