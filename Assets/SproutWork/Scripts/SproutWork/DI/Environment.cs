using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Yaroyan.SproutWork.Common;
using System.Linq;
using System.IO;
using System;
using Yaroyan.SproutWork.Infrastructure.DataSource.TypeHandler;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.SQLiteRepository.TypeHandler;
using Dapper;

[CreateAssetMenu(menuName = "MyScriptables/Create Environment")]
public class Environment : ScriptableObject
{
    [field: SerializeField] public DbConfig DbConfig { get; private set; }
}

[System.Serializable]
public class DbConfig
{
    public static readonly string s_queryDBAddress = "Assets/Addressables/Database/SQLite/QueryDB/QueryDB.bytes";
    public static readonly string s_eventStoreAddress = "Assets/Addressables/Database/LiteDB/EventStore/EventStore.bytes";
    public static readonly string s_queryDBMasterDataDDLAddress = "";
    public static readonly string s_queryDBMasterDataDMLAddress = "";
    public static readonly string s_dataSourceLabel = "dataSource";
    public static readonly string s_masterDataBaseName = "QueryDB";

#if UNITY_EDITOR
    public static string GetQueryDBPath() => Path.Combine(UnityEngine.Application.dataPath, "Addressables/Database/SQLite/QueryDB/QueryDB.bytes");
    public static string GetEventStorePath() => Path.Combine(UnityEngine.Application.dataPath, "Addressables/Database/LiteDB/EventStore/EventStore.bytes");
#else
    public static string GetQueryDBPath() => Path.Combine(UnityPathResolver.GetPlatformIndependentPersistentDataPath(), s_dataSourceLabel, s_queryDBAddress.Split("/").Last());
    public static string GetEventStorePath() => Path.Combine(UnityPathResolver.GetPlatformIndependentPersistentDataPath(), s_dataSourceLabel, s_eventStoreAddress.Split("/").Last());
#endif

    [field: SerializeField] internal QueryDBInstanceType QueryDBInstanceType { get; private set; }
    [field: SerializeField] internal EventStoreInstanceType EventStoreInstanceType { get; private set; }
}

internal enum QueryDBInstanceType
{
    SQLite,
    InMemorySQLite,
    Dictionary,
}

internal enum EventStoreInstanceType
{
    LiteDB,
    InMemoryLiteDB,
    Dictionary,
}

public class EventStoreUniOfWorkProvider
{
    readonly DbConfig _config;
    public EventStoreUniOfWorkProvider(DbConfig config)
    {
        _config = config;
    }
}

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void initialize()
    {
        //====================
        //  SQLite
        //====================
        SQLitePCL.Batteries_V2.Init();

        //====================
        //  Dapper
        //====================
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        SqlMapper.AddTypeHandler(new SceneIdHandler());
        SqlMapper.AddTypeHandler(new GuidHandler());

        //====================
        //  Addressables
        //====================
#if !UNITY_EDITOR
        CloneAddressableTextAssets(DbConfig.s_queryDBAddress, DbConfig.GetQueryDBPath());
        CloneAddressableTextAssets(DbConfig.s_eventStoreAddress, DbConfig.GetEventStorePath());
#endif
    }

    /// <summary>
    /// Synchronously load and copy files locally.
    /// </summary>
    /// <param name="address"></param>
    /// <param name="path"></param>
    static void CloneAddressableTextAssets(string address, string path)
    {
        var fileInfo = new FileInfo(path);
        fileInfo.Refresh();
        // If the target file exists locally, processing does not continue.
        if (fileInfo.Exists) return;
        fileInfo.Directory.Create();
        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        File.WriteAllBytes(fileInfo.FullName, handle.WaitForCompletion().bytes);
        Addressables.Release(handle);
    }

    /// <summary>
    /// Synchronously load and copy files locally.
    /// </summary>
    /// <param name="label"></param>
    /// <param name="callback"></param>
    static void CloneAddressableTextAssets(string label, System.Action<TextAsset> callback)
    {
        var directoryInfo = new DirectoryInfo(Path.Combine(UnityPathResolver.GetPlatformIndependentPersistentDataPath(), DbConfig.s_dataSourceLabel));
        directoryInfo.Refresh();
        // If the directory contains target files exists locally, processing does not continue.
        if (directoryInfo.Exists) return;
        directoryInfo.Create();
        var handle = Addressables.LoadAssetsAsync(label, callback);
        handle.WaitForCompletion();
        // Unity API cannot be called from Threads other than the Main Thread, so synchronous processing is performed.
        foreach (var asset in handle.Result) File.WriteAllBytes(Path.Combine(directoryInfo.FullName, asset.name), asset.bytes);
        Addressables.Release(handle);
    }
}

