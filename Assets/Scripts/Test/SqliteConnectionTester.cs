using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Sqlite;
using SqlKata.Execution;
using SqlKata.Compilers;
using UnityEngine.Networking;

/// <summary>
/// SQLite3�̐ڑ��m�F�N���X
/// </summary>
public class SqliteConnectionTester : MonoBehaviour
{
    // DB��
    static readonly string s_dbName = "database.sqlite3";
    // �e�[�u����
    static readonly string s_tableName = "Scenes";
    // �J������
    static readonly string s_columnName = "Id";

    void Start()
    {
        CloneDB();

        SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder
        {
            DataSource = System.IO.Path.Combine(GetPlatFormDataPath(), s_dbName)
        };

        SQLitePCL.Batteries_V2.Init();

        Debug.Log("�ڑ��m�F");

        SqliteConnection connection = new SqliteConnection(builder.ConnectionString);

        // SQLite�̃o�[�W�����o��
        Debug.Log("�o�[�W�������F" + connection.ServerVersion);

        SqliteCompiler compiler = new SqliteCompiler();
        QueryFactory factory = new QueryFactory(connection, compiler);

        Debug.Log("���R�[�h�o�́F" + s_tableName);
        // scenes�e�[�u������id�J�����̃f�[�^���擾����B
        foreach (System.Object obj in factory.Query(s_tableName).Select(s_columnName).Get<System.Object>())
        {
            // �e�[�u������f�[�^���擾�ł��Ă��邩�m�F
            Debug.Log(obj.ToString());
        }
    }

    string GetPlatFormDataPath()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
        {
            return getFilesDir.Call<string>("getCanonicalPath");
        }
#else
        return Application.persistentDataPath;
#endif
    }

    void CloneDB()
    {
        string targetPath = System.IO.Path.Combine(GetPlatFormDataPath(), s_dbName);
        if (System.IO.File.Exists(targetPath)) return;
        string sourcePath = System.IO.Path.Combine(Application.streamingAssetsPath, s_dbName);

#if !UNITY_EDITOR && UNITY_ANDROID
        StartCoroutine(Copy(sourcePath, targetPath));
#else
        System.IO.File.Copy(sourcePath, targetPath);
#endif

#pragma warning disable CS8321 // ���[�J���֐��͐錾����Ă��܂����A��x���g�p����Ă��܂���
        IEnumerator Copy(string sourcePath, string targetPath)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(sourcePath))
            {
                yield return request.SendWebRequest();
                System.IO.File.WriteAllBytes(targetPath, request.downloadHandler.data);
            }
        }
#pragma warning restore CS8321 // ���[�J���֐��͐錾����Ă��܂����A��x���g�p����Ă��܂���
    }
}
