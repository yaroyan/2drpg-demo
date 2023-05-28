using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaroyan.SproutWork.Domain.Model.Constant;

namespace Yaroyan.SproutWork.Domain.Model.Constant
{
    [System.Serializable]
    public class Scene : ScriptableObject
    {
        [SerializeField] public int Id { get; private set; }
        [SerializeField] public string Name { get; private set; }
        [SerializeField] public Scene ParentScene { get; private set; }
        [SerializeField] public int BuildIndex { get; private set; }
    }
}
