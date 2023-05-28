using System.Collections;
using System.Collections.Generic;
using System;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SproutWork.Domain.Model.Scene
{
    public record SceneContext(int BuildIndex, string SceneName) : ValueObject
    {
        public int BuildIndex { get; private init; } = ValidateBuildIndex(BuildIndex);
        public string SceneName { get; private init; } = ValidateSceneName(SceneName);
        static readonly int s_MinBuildIndex = 0;
        static readonly int s_MaxSceneNameCharacterCount = 245;

        static int ValidateBuildIndex(int buildIndex)
        {
            if (buildIndex < s_MinBuildIndex) throw new ArgumentException($"must be positive or {s_MinBuildIndex}. Build index: {buildIndex}", nameof(BuildIndex));
            return buildIndex;
        }

        static string ValidateSceneName(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName)) throw new ArgumentException($"must not be null or empty or only whitespaces.", nameof(SceneName));
            if (sceneName.Length >= s_MaxSceneNameCharacterCount) throw new ArgumentException($"character is less than {s_MaxSceneNameCharacterCount}. Character count: {sceneName.Length} Name: {sceneName}", nameof(SceneName));
            return sceneName;
        }

        public SceneContext ChangeBuildIndex(int buildIndex)
        {
            return this with { BuildIndex = ValidateBuildIndex(buildIndex) };
        }

        public SceneContext ChangeSceneName(string sceneName)
        {
            return this with { SceneName = ValidateSceneName(sceneName) };
        }
    }
}