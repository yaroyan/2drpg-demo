using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue
{
    [System.Serializable]
    public class BranchData : BaseData
    {
        public string trueGuidNode;
        public string falseGuidNode;
        public List<EventDataStringCondition> EventData_StringConditions = new List<EventDataStringCondition>();

        public BranchData(Ulid NodeGuid) : base(NodeGuid)
        {
        }
    }
}
