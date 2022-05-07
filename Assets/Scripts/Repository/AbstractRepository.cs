using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Com.Github.Yaroyan.Rpg.Sql;
using UnityEngine;
using UnityEngine.Networking;
using SqlKata.Execution;
using SqlKata.Compilers;
using Microsoft.Data.Sqlite;
using UnityEngine.AddressableAssets;

namespace Com.Github.Yaroyan.Rpg.Repository
{
    public abstract class AbstractRepository
    {
        protected readonly ISqliteConfig sqliteConfig;

        [VContainer.Inject]
        public AbstractRepository(ISqliteConfig sqliteConfig)
        {
            this.sqliteConfig = sqliteConfig;
        }

        public void TestFind()
        {
            using (SqliteConnection connection = new SqliteConnection(this.sqliteConfig.createBuilder().ConnectionString))
            {
                Debug.Log(this.sqliteConfig.createBuilder().ConnectionString);
                var compiler = new SqlServerCompiler();
                var db = new QueryFactory(connection, compiler);
                // scenesテーブルからidカラムのデータを取得する。
                foreach (System.Object obj in db.Query("Scenes").Select("Id").Get<System.Object>())
                {
                    // テーブルからデータを取得できているか確認
                    Debug.Log(obj.ToString());
                }
            }
        }

        public IEnumerable<T> FindAll<T>()
        {
            return Enumerable.Empty<T>();
        }

        public T FindById<T>(string id)
        {
            return default(T);
        }

        public IEnumerable<T> Find<T>(IEnumerable<SqlConnection> conditions)
        {
            return Enumerable.Empty<T>();
        }

        public void Save()
        {

        }

        public void Update()
        {

        }

        public void Delete()
        {

        }

        public bool Exists()
        {
            return false;
        }

        public bool NotExists()
        {
            return !Exists();
        }
    }
}