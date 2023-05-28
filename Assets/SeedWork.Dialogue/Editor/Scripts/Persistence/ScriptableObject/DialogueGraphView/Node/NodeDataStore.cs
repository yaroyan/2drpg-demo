using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class NodeDataStore : ScriptableObject
    {
        public List<NodeLinkData> NodeLinkDatas { get; } = new();
        public List<NodeData<EndData>> EndDatas { get; } = new();
        public List<NodeData<StartData>> StartDatas { get; } = new();
        public List<NodeData<EventData>> EventDatas { get; } = new();
        public List<NodeData<BranchData>> BranchDatas { get; } = new();
        public List<NodeData<DialogueData>> DialogueDatas { get; } = new();
        public List<NodeData<ChoiceData>> ChoiceDatas { get; } = new();

        public IEnumerable<BaseNodeData> AllDatas
        {
            get
            {
                List<BaseNodeData> links = new();
                links.AddRange(EndDatas);
                links.AddRange(StartDatas);
                links.AddRange(EventDatas);
                links.AddRange(BranchDatas);
                links.AddRange(DialogueDatas);
                links.AddRange(ChoiceDatas);
                return links;
            }
        }
    }
}
