using VContainer;
using VContainer.Unity;
using Com.Github.Yaroyan.Rpg;
using Com.Github.Yaroyan.Rpg.Repository;

namespace Com.Github.Yaroyan.Rpg.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [UnityEngine.SerializeField] Environment _environment;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISqliteConfig>(_ => _environment.DbConfig.IsInMemory ? new InMemorySqliteConfig() : new SqliteConfig(GetPlatFormDataPath()), Lifetime.Singleton);
            builder.Register<CharacterRepository>(Lifetime.Singleton);
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
            return UnityEngine.Application.persistentDataPath;
#endif
        }
    }
}