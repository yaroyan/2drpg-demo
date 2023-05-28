using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue
{
    [Serializable]
    public abstract class BaseData
    {
        [field: SerializeField]
        public string NodeGuid { get; private set; }
        public Vector2 Position;

        public BaseData(Ulid NodeGuid)
        {
            this.NodeGuid = NodeGuid.ToString();
        }
    }
}
