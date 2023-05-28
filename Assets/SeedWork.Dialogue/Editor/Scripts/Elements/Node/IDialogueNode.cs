using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal interface IDialogueNode
    {
        public void MutateOnLoad();
    }
    internal interface IGnericDialogueNode<T> : IDialogueNode where T : BaseData { }
}
