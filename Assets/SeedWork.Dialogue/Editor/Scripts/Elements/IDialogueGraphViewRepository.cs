using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue.Editor.Domain.GraphView
{
    internal interface IDialogueGraphViewRepository
    {
        public void Save(DialogueGraphView graphView);
        public void Load(DialogueGraphView graphView);
    }
}
