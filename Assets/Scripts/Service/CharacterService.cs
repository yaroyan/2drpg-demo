using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Github.Yaroyan.Rpg.Repository;
using SqlKata.Compilers;
using SqlKata.Execution;
using Microsoft.Data.Sqlite;

namespace Com.Github.Yaroyan.Rpg.Service
{
    public class CharacterService : MonoBehaviour
    {
        [SerializeField] CharacterRepository characterRepository;
        [SerializeField] GameObject characterPrefab;
        [SerializeField] Text text;
        QueryFactory factory;
        // Start is called before the first frame update
        void Start()
        {
            characterRepository = new CharacterRepository();
            characterRepository.TestFind();
        }

        void FileCopy(string baseFilePath, string tempFilePath)
        {
            if (System.IO.File.Exists(tempFilePath) == false)
            {
                if (baseFilePath.Contains("://"))
                {
                    // Androidの場合
                    StartCoroutine(FileCopyForAndroid(baseFilePath, tempFilePath));
                }
                else
                {
                    System.IO.File.Copy(baseFilePath, tempFilePath);
                }
            }
        }

        IEnumerator FileCopyForAndroid(string baseFilePath, string tempFilePath)
        {
            using (var webRequest = UnityEngine.Networking.UnityWebRequest.Get(baseFilePath))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    // エラー処理
                    yield break;
                }

                System.IO.File.WriteAllBytes(tempFilePath, webRequest.downloadHandler.data);
            }
        }

        public void Test()
        {
            string pathDB = null;
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
            {
                string secureDataPathForAndroid = getFilesDir.Call<string>("getCanonicalPath");
                pathDB = System.IO.Path.Combine(secureDataPathForAndroid, "database.sqlite3");
            }
            string sourcePath = System.IO.Path.Combine(Application.streamingAssetsPath, "database.sqlite3");
            FileCopy(sourcePath, pathDB);
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder
            {
                DataSource = System.IO.Path.Combine(Application.streamingAssetsPath, "database.sqlite3")
            };
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            // SQLitePCL.Batteries_V2.Init();
            SqliteConnection connection = new SqliteConnection(builder.ConnectionString);
            SqliteCompiler compiler = new SqliteCompiler();
            QueryFactory factory = new QueryFactory(connection, compiler);
            foreach (System.Object obj in factory.Query("Scenes").Select("Id").Get<System.Object>())
            {
                Debug.Log(obj.ToString());
            }
        }

        public void Test2()
        {
            string path2 = System.IO.Path.Combine(Application.streamingAssetsPath, "database.sqlite3");
            Debug.Log(path2);
            Debug.Log(System.IO.File.Exists(path2));
            string path = System.IO.Path.Combine(Application.persistentDataPath, "database.sqlite3");
            Debug.Log(path);
            Debug.Log(System.IO.File.Exists(path));
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
            {
                string secureDataPathForAndroid = getFilesDir.Call<string>("getCanonicalPath");
                Debug.Log(System.IO.File.Exists(System.IO.Path.Combine(secureDataPathForAndroid, "database.sqlite3")));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// IDからキャラクターを生成します。
        /// </summary>
        /// <param name="id">キャラクターID</param>
        /// <returns></returns>
        public GameObject CreateCharacter(string id)
        {
            characterRepository.FindById<System.Object>(id);
            GameObject gameObject = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
            gameObject.name = "player";
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
            gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("");
            return gameObject;
        }
    }
}
