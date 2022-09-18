# if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SqlKata.Execution;
using SqlKata.Compilers;
using System.Data;
using Dapper;

namespace Yaroyan.SproutWork.EditorExtension.IXporter
{
    [CustomEditor(typeof(DBIXporter))]
    public class DBIXporterEditor : Editor
    {
        DBIXporter importer;
        void OnEnable()
        {
            this.importer = target as DBIXporter;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Import"))
            {
                using (IDbConnection connection = importer.Connector.ProvideConnection())
                {
                    connection.Open();
                    Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                    QueryFactory factory = new QueryFactory(connection, new SqliteCompiler());
                    foreach (var obj in factory.Query("scenes").Get())
                    {
                        Debug.Log(obj.BuildIndex);
                    }
                }
            }
        }

        private void CreateAsset()
        {
            //var guids = AssetDatabase.FindAssets("t:");
            //if (guids.Length == 0)
            //{
            //    throw new System.IO.FileNotFoundException("MyScriptableObject does not found");
            //}

            //var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            //var obj = AssetDatabase.LoadAssetAtPath<MyScriptableObject>(path);
        }
    }
}

#endif