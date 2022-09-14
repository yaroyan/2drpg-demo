using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SqlKata.Compilers;
using SqlKata.Execution;
using Microsoft.Data.Sqlite;
using UnityEngine.Networking;
using Yaroyan.Game.RPG.Infrastructure.DataSource;
using VContainer;
using System;
using Yaroyan.Game.RPG.Domain.Model.Scene;

/// <summary>
/// SQLite3の接続確認クラス
/// </summary>
public class SqliteConnectionTester : MonoBehaviour
{
    Func<IUnitOfWork> unitOfWorkSupplier;

    [Inject]
    public void inject(Func<IUnitOfWork> supplier)
    {
        unitOfWorkSupplier = supplier;
    }

    void Start()
    {
        using (IUnitOfWork uow = unitOfWorkSupplier.Invoke())
        {
            foreach (Scene scene in uow.SceneRepository.FindAll())
            {
                Debug.Log(scene.SceneContext.SceneName);
            }
        }
        //foreach (var scene in unitOfWork.SceneRepository.FindAll()) Debug.Log(scene.Id);

    }
}
