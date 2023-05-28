using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue Editor/New Dialogue")]
    [System.Serializable]
    public class DialogueContainerSO : ScriptableObject
    {
        public List<NodeLinkData> NodeLinkDatas = new();

        public List<EndData> EndDatas = new();
        public List<StartData> StartDatas = new();
        public List<EventData> EventDatas = new();
        public List<BranchData> BranchDatas = new();
        public List<DialogueData> DialogueDatas = new();
        public List<ChoiceData> ChoiceDatas = new();

        public IReadOnlyCollection<BaseData> AllDatas
        {
            get
            {
                List<BaseData> links = new();
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