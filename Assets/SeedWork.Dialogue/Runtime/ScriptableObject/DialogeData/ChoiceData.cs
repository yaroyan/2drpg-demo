using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;
using System;

namespace Yaroyan.SeedWork.Dialogue
{
    [System.Serializable]
    public class ChoiceData : BaseData
    {
#if UNITY_EDITOR
        public TextField TextField { get; set; }
        public ObjectField ObjectField { get; set; }
#endif
        //public Container_ChoiceStateType ChoiceStateTypes = new Container_ChoiceStateType();
        public Ref<ChoiceStateType> ChoiceStateType { get; set; } = new Ref<ChoiceStateType>() { Value = Dialogue.ChoiceStateType.Hide };
        public List<LanguageGeneric<string>> Text = new List<LanguageGeneric<string>>();
        public List<LanguageGeneric<AudioClip>> AudioClips = new List<LanguageGeneric<AudioClip>>();
        public List<EventDataStringCondition> EventData_StringConditions = new List<EventDataStringCondition>();

        public ChoiceData(Ulid NodeGuid) : base(NodeGuid)
        {
        }
    }
}
