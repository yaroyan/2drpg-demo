using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaroyan.SeedWork.Dialogue.Editor.Infrastructure;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class GraphViewInspector
    {
        DialogueGraphView _graphview;
        DictionaryNodeRepository<StartNode> _startNodeRepository = new();
        DictionaryNodeRepository<EndNode> _endNodeRepository = new();
        DictionaryNodeRepository<DialogueNode> _dialogueNodeRepository = new();
        DictionaryNodeRepository<ChoiceNode> _choideNodeRepository = new();
        DictionaryNodeRepository<EventNode> _eventNodeRepository = new();
        DictionaryNodeRepository<BranchNode> _branchNodeRepository = new();

        public GraphViewInspector(DialogueGraphView graphView)
        {
            _graphview = graphView;
        }
    }
}
