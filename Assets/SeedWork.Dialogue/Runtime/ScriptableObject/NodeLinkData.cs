using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue
{
    [Serializable]
    public class NodeLinkData
    {
        [field: SerializeField]
        public string BaseNodeGuid { get; private set; }
        [field: SerializeField]
        public string BasePortName { get; private set; }
        [field: SerializeField]
        public string TargetNodeGuid { get; private set; }
        [field: SerializeField]
        public string TargetPortName { get; private set; }

        public NodeLinkData(Ulid baseNodeGuid, string basePortName, Ulid targetNodeGuid, string targetPortName)
        {
            BaseNodeGuid = baseNodeGuid.ToString();
            BasePortName = basePortName;
            TargetNodeGuid = targetNodeGuid.ToString();
            TargetPortName = targetPortName;
        }
    }
}
