using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;
using System;

namespace Yaroyan.SeedWork.Dialogue
{
    [System.Serializable]
    public class DialogueData : BaseData
    {
        public List<DialogueData_BaseContainer> Dialogue_BaseContainers { get; set; } = new();
        public List<DialogueData_Name> DialogueData_Names = new();
        public List<DialogueData_Text> DialogueData_Texts = new();
        public List<DialogueData_Images> DialogueData_Imagess = new();
        public List<DialogueData_Port> DialogueData_Ports = new();

        public DialogueData(Ulid NodeGuid) : base(NodeGuid) { }
    }

    [System.Serializable]
    public class DialogueData_BaseContainer
    {
        public Ref<int> ID = new();
    }

    [System.Serializable]
    public class DialogueData_Name : DialogueData_BaseContainer
    {
        public Ref<string> CharacterName = new();
    }

    [System.Serializable]
    public class DialogueData_Text : DialogueData_BaseContainer
    {
#if UNITY_EDITOR
        public TextField TextField { get; set; }
        public ObjectField ObjectField { get; set; }
#endif
        public Ref<string> GuidID = new();
        public List<LanguageGeneric<string>> Text = new List<LanguageGeneric<string>>();
        public List<LanguageGeneric<AudioClip>> AudioClips = new List<LanguageGeneric<AudioClip>>();
    }

    [System.Serializable]
    public class DialogueData_Images : DialogueData_BaseContainer
    {
        public Ref<Sprite> Sprite_Left = new();
        public Ref<Sprite> Sprite_Right = new();
    }

    [System.Serializable]
    public class DialogueData_Port
    {
        public string PortGuid;
        public string InputGuid;
        public string OutputGuid;
    }
}
