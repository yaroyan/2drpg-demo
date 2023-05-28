using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yaroyan.SproutWork.Domain.Model.Constant
{
    /// <summary>
    /// Sceneの遷移地点クラス
    /// </summary>
    public class Entrance : ScriptableObject
    {
        [SerializeField] string _name;
        [SerializeField] Scene _scene;
        [SerializeField] string _description;
        [SerializeField] bool _isWarpPoint;
        [SerializeField] bool _isLandmark;
        [SerializeField] Guid _guid = Guid.NewGuid();
    }
}
