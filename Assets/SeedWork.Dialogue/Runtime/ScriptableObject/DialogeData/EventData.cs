using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue
{
    [System.Serializable]
    public class EventData : BaseData
    {
        public List<EventDataStringModifier> EventDataStringModifiers { get; set; } = new();
        //public List<Container_DialogueEventSO> Container_DialogueEventSOs = new List<Container_DialogueEventSO>();
        public List<Ref<DialogueEventSO>> DialogueEventSOs { get; set; } = new();

        public EventData(Ulid NodeGuid) : base(NodeGuid)
        {
        }
    }
}
