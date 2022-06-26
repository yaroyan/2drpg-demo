# if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SqlKata.Execution;
using SqlKata.Compilers;
using System.Data;
using Dapper;

namespace Yaroyan.Game.EditorExtension.Importer
{
    [CustomEditor(typeof(DatabaseImporter))]
    public class DatabaseImporterEditor : Editor
    {
        DatabaseImporter importer;
        void OnEnable()
        {
            this.importer = target as DatabaseImporter;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Import"))
            {
                using (IDbConnection connection = importer.Connector.ProvideConnection())
                {
                    connection.Open();
                    // �J�������̃A���_�[�X�R�A�𖳎�����B
                    // Dapper�̓f�t�H���g�ő啶���Ə������𖳎�����B
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