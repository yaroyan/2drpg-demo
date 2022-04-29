using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Com.Github.Yaroyan.Rpg.Sql;
using UnityEngine;
using SqlKata.Execution;
using SqlKata.Compilers;
using Microsoft.Data.Sqlite;

namespace Com.Github.Yaroyan.Rpg.Repository
{
    public abstract class AbstractRepository
    {
        protected readonly QueryFactory _factory;
        public AbstractRepository()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder
            {
                DataSource = System.IO.Path.Combine(Application.streamingAssetsPath, "database.sqlite3")
            };
            SqliteConnection connection = new SqliteConnection(builder.ConnectionString);
            SqliteCompiler compiler = new SqliteCompiler();
            QueryFactory factory = new QueryFactory(connection, compiler);
            _factory = factory;
        }

        public void TestFind()
        {
            foreach (System.Object obj in _factory.Query("Scenes").Select("Id").Get<System.Object>())
            {
                Debug.Log(obj.ToString());
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