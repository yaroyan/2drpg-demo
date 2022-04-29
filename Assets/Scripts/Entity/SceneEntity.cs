using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntity
{
    public static class Column
    {
        public static readonly string Id = "id";
        public static readonly string Name = "name";
        public static readonly string BuildIndex = "build_index";
    }
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int BuildIndex { get; private set; }
}
