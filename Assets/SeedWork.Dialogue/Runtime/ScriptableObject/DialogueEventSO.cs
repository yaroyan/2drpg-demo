using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue
{
    [System.Serializable]
    public class DialogueEventSO : ScriptableObject
    {
        public virtual void RunEvent()
        {
            Debug.Log("Event was Call");
        }
    }
}