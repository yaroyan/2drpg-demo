using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue
{
    [System.Serializable]
    public class EndData : BaseData
    {
        //public Container_EndNodeType EndNodeType = new Container_EndNodeType();
        public Ref<EndNodeType> EndNodeType { get; set; } = new Ref<EndNodeType>{ Value = Dialogue.EndNodeType.End };

        public EndData(Ulid NodeGuid) : base(NodeGuid)
        {
        }
    }
}