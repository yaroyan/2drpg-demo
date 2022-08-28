using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.Game.RPG.Helper
{
    public static class UnityPathHelper
    {

        public static string GetPlatformIndependentDataPath()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
        {
            return getFilesDir.Call<string>("getCanonicalPath");
        }
#else
            return UnityEngine.Application.persistentDataPath;
#endif
        }

        /// <summary>
        /// Get the platform-independent StreamingAssetsPath.
        /// </summary>
        /// <returns>stremingAssetsPath</returns>
        public static string GetPlatformIndependentStreamingAssetsPath()
        {

#if !UNITY_EDITOR && UNITY_ANDROID
            return GetAndroidStreamingAssetsPath();
#else
            return UnityEngine.Application.streamingAssetsPath;
#endif
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
