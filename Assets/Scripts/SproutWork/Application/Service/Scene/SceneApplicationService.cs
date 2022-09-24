using System.Collections;
using System.Collections.Generic;
using Yaroyan.SproutWork.Domain.Model.Scene;
using VContainer;
using System;

/// <summary>
/// Application service for Scene entity.
/// </summary>
public class SceneApplicationService
{
    readonly ISceneRepository _sceneRepository;
    readonly SceneDomainService _sceneDomainService;

    [Inject]
    public SceneApplicationService(ISceneRepository sceneRepository)
    {
        _sceneRepository = sceneRepository;
        _sceneDomainService = new SceneDomainService(_sceneRepository);
    }

    public void RegisterScene(string parentId, List<string> locations, int buildIndex, string sceneName)
    {
        SceneContext context = new SceneContext(buildIndex, sceneName);
        Scene scene = new Scene(_sceneRepository.NextIdentity(), context, new SceneId(parentId));
        if (_sceneDomainService.isDuplicated(scene)) throw new InvalidOperationException($"Scene is Duplicated. {scene}");
        _sceneRepository.Save(scene);
    }

    public void UnRegisterScene(SceneId id)
    {
        Scene scene = _sceneRepository.Find(id);
        if (scene is null) throw new InvalidOperationException($"Not found. Scene id: {id}");
        _sceneRepository.Delete(scene.Id);
    }
}
