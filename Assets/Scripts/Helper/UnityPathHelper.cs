using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.Game.RPG.Helper
{
    public static class UnityPathHelper
    {
        static string s_platformIndependentPersistentDataPath;
        static string s_platformIndependentStreamingAssetsPath;
        public static string GetPlatformIndependentPersistentDataPath()
        {
            if (s_platformIndependentPersistentDataPath is null)
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
                {
                    s_platformIndependentPersistentDataPath = getFilesDir.Call<string>("getCanonicalPath");
                }
#else
                s_platformIndependentPersistentDataPath = UnityEngine.Application.persistentDataPath;
#endif
            }
            return s_platformIndependentPersistentDataPath;
        }

        /// <summary>
        /// Get the platform-independent StreamingAssetsPath.
        /// </summary>
        /// <returns>stremingAssetsPath</returns>
        public static string GetPlatformIndependentStreamingAssetsPath()
        {
            if (s_platformIndependentStreamingAssetsPath is null)
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                s_platformIndependentStreamingAssetsPath = GetAndroidStreamingAssetsPath();
#else
                s_platformIndependentStreamingAssetsPath = UnityEngine.Application.streamingAssetsPath;
#endif
            }
            return s_platformIndependentStreamingAssetsPath;
        }

        /// <summary>
        /// Get Android's StreamingAssetsPath.
        /// </summary>
        /// <returns>streamingAssetsPath</returns>
        public static string GetAndroidStreamingAssetsPath()
        {
            using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(UnityEngine.Application.streamingAssetsPath))
            {
                request.SendWebRequest();
                // Force asynchronous processing to synchronous processing.
                while (!request.isDone) { }
                return request.downloadHandler.text;
            }
        }
    }
}
