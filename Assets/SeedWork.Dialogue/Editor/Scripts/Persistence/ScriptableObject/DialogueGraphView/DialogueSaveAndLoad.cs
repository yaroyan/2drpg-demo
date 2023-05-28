using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class DialogueSaveAndLoad
    {
        private List<Edge> edges => graphView.edges.ToList();
        private List<BaseNode> nodes => graphView.nodes.ToList().Where(node => node is BaseNode).Cast<BaseNode>().ToList();

        private DialogueGraphView graphView;

        private DialogueNodeFactory _nodeFactory;

        internal DialogueSaveAndLoad(DialogueGraphView graphView, DialogueNodeFactory nodeFactory)
        {
            this.graphView = graphView;
            this._nodeFactory = nodeFactory;
        }

        public void Save(DialogueContainerSO dialogueContainerSO)
        {
            SaveEdges(dialogueContainerSO);
            SaveNodes(dialogueContainerSO);

            EditorUtility.SetDirty(dialogueContainerSO);
            AssetDatabase.SaveAssets();
        }

        public void Load(DialogueContainerSO dialogueContainerSO)
        {
            ClearGraph();
            GenerateNodes(dialogueContainerSO);
            ConnectNodes(dialogueContainerSO);
        }

        #region Save
        private void SaveEdges(DialogueContainerSO dialogueContainerSO)
        {
            dialogueContainerSO.NodeLinkDatas.Clear();

            Edge[] connectedEdges = edges.Where(edge => edge.input.node is not null).ToArray();
            for (int i = 0; i < connectedEdges.Count(); i++)
            {
                BaseNode outputNode = (BaseNode)connectedEdges[i].output.node;
                BaseNode inputNode = connectedEdges[i].input.node as BaseNode;

                dialogueContainerSO.NodeLinkDatas.Add(
                    new NodeLinkData(outputNode.NodeId, connectedEdges[i].output.portName, inputNode.NodeId, connectedEdges[i].input.portName)
                );
            }
        }

        private void SaveNodes(DialogueContainerSO dialogueContainerSO)
        {
            dialogueContainerSO.EventDatas.Clear();
            dialogueContainerSO.EndDatas.Clear();
            dialogueContainerSO.StartDatas.Clear();
            dialogueContainerSO.BranchDatas.Clear();
            dialogueContainerSO.DialogueDatas.Clear();
            dialogueContainerSO.ChoiceDatas.Clear();

            nodes.ForEach(node =>
            {
                switch (node)
                {
                    case DialogueNode dialogueNode:
                        dialogueContainerSO.DialogueDatas.Add(SaveNodeData(dialogueNode));
                        break;
                    case StartNode startNode:
                        dialogueContainerSO.StartDatas.Add(SaveNodeData(startNode));
                        break;
                    case EndNode endNode:
                        dialogueContainerSO.EndDatas.Add(SaveNodeData(endNode));
                        break;
                    case EventNode eventNode:
                        dialogueContainerSO.EventDatas.Add(SaveNodeData(eventNode));
                        break;
                    case BranchNode branchNode:
                        dialogueContainerSO.BranchDatas.Add(SaveNodeData(branchNode));
                        break;
                    case ChoiceNode choiceNode:
                        dialogueContainerSO.ChoiceDatas.Add(SaveNodeData(choiceNode));
                        break;
                    default:
                        break;
                }
            });
        }

        private DialogueData SaveNodeData(DialogueNode node)
        {
            DialogueData dialogueData = new DialogueData(node.NodeId)
            {
                Position = node.GetPosition().position,
            };

            // Set ID
            for (int i = 0; i < node.DialogueData.Dialogue_BaseContainers.Count; i++)
            {
                node.DialogueData.Dialogue_BaseContainers[i].ID.Value = i;
            }

            foreach (DialogueData_BaseContainer baseContainer in node.DialogueData.Dialogue_BaseContainers)
            {
                // Name
                if (baseContainer is DialogueData_Name)
                {
                    DialogueData_Name tmp = (baseContainer as DialogueData_Name);
                    DialogueData_Name tmpData = new DialogueData_Name();

                    tmpData.ID.Value = tmp.ID.Value;
                    tmpData.CharacterName.Value = tmp.CharacterName.Value;

                    dialogueData.DialogueData_Names.Add(tmpData);
                }

                // Text
                if (baseContainer is DialogueData_Text)
                {
                    DialogueData_Text tmp = (baseContainer as DialogueData_Text);
                    DialogueData_Text tmpData = new DialogueData_Text();

                    tmpData.ID = tmp.ID;
                    tmpData.GuidID = tmp.GuidID;
                    tmpData.Text = tmp.Text;
                    tmpData.AudioClips = tmp.AudioClips;

                    dialogueData.DialogueData_Texts.Add(tmpData);
                }

                // Images
                if (baseContainer is DialogueData_Images)
                {
                    DialogueData_Images tmp = (baseContainer as DialogueData_Images);
                    DialogueData_Images tmpData = new DialogueData_Images();

                    tmpData.ID.Value = tmp.ID.Value;
                    tmpData.Sprite_Left.Value = tmp.Sprite_Left.Value;
                    tmpData.Sprite_Right.Value = tmp.Sprite_Right.Value;

                    dialogueData.DialogueData_Imagess.Add(tmpData);
                }
            }

            // Port
            foreach (DialogueData_Port port in node.DialogueData.DialogueData_Ports)
            {
                DialogueData_Port portData = new DialogueData_Port();

                portData.PortGuid = port.PortGuid;

                foreach (Edge edge in edges)
                {
                    if (edge.output.portName == port.PortGuid.ToString())
                    {
                        portData.OutputGuid = (edge.output.node as BaseNode).NodeId.ToString();
                        portData.InputGuid = (edge.input.node as BaseNode).NodeId.ToString();
                    }
                }

                dialogueData.DialogueData_Ports.Add(portData);
            }

            return dialogueData;
        }

        private StartData SaveNodeData(StartNode node)
        {
            StartData nodeData = new StartData(node.NodeId)
            {
                Position = node.GetPosition().position,
            };

            return nodeData;
        }

        private EndData SaveNodeData(EndNode node)
        {
            EndData nodeData = new EndData(node.NodeId)
            {
                Position = node.GetPosition().position,
            };
            nodeData.EndNodeType.Value = node.EndData.EndNodeType.Value;

            return nodeData;
        }

        private EventData SaveNodeData(EventNode node)
        {
            EventData nodeData = new EventData(node.NodeId)
            {
                Position = node.GetPosition().position,
            };

            // Save Dialogue Event
            foreach (Ref<DialogueEventSO> dialogueEvent in node.EventData.DialogueEventSOs)
            {
                nodeData.DialogueEventSOs.Add(dialogueEvent);
            }

            // Save String Event
            foreach (EventDataStringModifier stringEvents in node.EventData.EventDataStringModifiers)
            {
                EventDataStringModifier tmp = new EventDataStringModifier();
                tmp.Number.Value = stringEvents.Number.Value;
                tmp.StringEventText.Value = stringEvents.StringEventText.Value;
                tmp.StringEventModifierType.Value = stringEvents.StringEventModifierType.Value;

                nodeData.EventDataStringModifiers.Add(tmp);
            }

            return nodeData;
        }

        private BranchData SaveNodeData(BranchNode node)
        {
            List<Edge> tmpEdges = edges.Where(x => x.output.node == node).Cast<Edge>().ToList();

            Edge trueOutput = edges.FirstOrDefault(x => x.output.node == node && x.output.portName == "True");
            Edge falseOutput = edges.FirstOrDefault(x => x.output.node == node && x.output.portName == "False");


            BranchData nodeData = new BranchData(node.NodeId)
            {
                Position = node.GetPosition().position,
                trueGuidNode = (trueOutput?.input.node as BaseNode)?.NodeId.ToString() ?? null,
                falseGuidNode = (falseOutput?.input.node as BaseNode)?.NodeId.ToString() ?? null,
            };

            foreach (EventDataStringCondition stringEvents in node.BranchData.EventData_StringConditions)
            {
                EventDataStringCondition tmp = new EventDataStringCondition();
                tmp.Number.Value = stringEvents.Number.Value;
                tmp.StringEventText.Value = stringEvents.StringEventText.Value;
                tmp.StringEventConditionType.Value = stringEvents.StringEventConditionType.Value;

                nodeData.EventData_StringConditions.Add(tmp);
            }

            return nodeData;
        }

        private ChoiceData SaveNodeData(ChoiceNode node)
        {
            ChoiceData nodeData = new ChoiceData(node.NodeId)
            {
                Position = node.GetPosition().position,

                Text = node.ChoiceData.Text,
                AudioClips = node.ChoiceData.AudioClips,
            };
            nodeData.ChoiceStateType.Value = node.ChoiceData.ChoiceStateType.Value;

            foreach (EventDataStringCondition stringEvents in node.ChoiceData.EventData_StringConditions)
            {
                EventDataStringCondition tmp = new EventDataStringCondition();
                tmp.StringEventText.Value = stringEvents.StringEventText.Value;
                tmp.Number.Value = stringEvents.Number.Value;
                tmp.StringEventConditionType.Value = stringEvents.StringEventConditionType.Value;

                nodeData.EventData_StringConditions.Add(tmp);
            }

            return nodeData;
        }
        #endregion

        #region Load

        private void ClearGraph()
        {
            foreach (var edge in edges) graphView.RemoveElement(edge);
            foreach (BaseNode node in nodes) graphView.RemoveElement(node);
        }

        private void GenerateNodes(DialogueContainerSO dialogueContainer)
        {
            UniTask.WhenAll(
                // Start
                DrawStartNode(dialogueContainer.StartDatas),
                // End Node 
                DrawEndNode(dialogueContainer.EndDatas),
                // Event Node
                DrawEventNode(dialogueContainer.EventDatas),
                // Breach Node
                DrawBranchNode(dialogueContainer.BranchDatas),
                // Choice Node
                DrawChoiceNode(dialogueContainer.ChoiceDatas),
                // Dialogue Node
                DrawDialogueNode(dialogueContainer.DialogueDatas)
            );
        }

        async UniTask DrawStartNode(IEnumerable<StartData> startDatas)
        {
            foreach (var chunk in startDatas.Chunk(100))
            {
                foreach (var data in chunk)
                {
                    graphView.AddElement(_nodeFactory.CreateNode<StartNode>(data.Position));
                }
                await UniTask.Yield();
            }
        }

        async UniTask DrawEndNode(IEnumerable<EndData> endDatas)
        {
            foreach (var chunk in endDatas.Chunk(100))
            {
                foreach (EndData data in chunk)
                {
                    EndNode node = _nodeFactory.CreateNode<EndNode>(data.Position);
                    node.EndData.EndNodeType.Value = data.EndNodeType.Value;

                    node.LoadValueInToField();
                    graphView.AddElement(node);
                }
                await UniTask.Yield();
            }
        }

        async UniTask DrawEventNode(IEnumerable<EventData> eventDatas)
        {
            foreach (var chunk in eventDatas.Chunk(100))
            {
                foreach (EventData data in chunk)
                {
                    EventNode node = _nodeFactory.CreateNode<EventNode>(data.Position);

                    foreach (Ref<DialogueEventSO> item in data.DialogueEventSOs)
                    {
                        node.AddScriptableEvent(item);
                    }
                    foreach (EventDataStringModifier item in data.EventDataStringModifiers)
                    {
                        node.AddStringEvent(item);
                    }

                    node.LoadValueInToField();
                    graphView.AddElement(node);
                }
                await UniTask.Yield();
            }
        }

        async UniTask DrawBranchNode(IEnumerable<BranchData> branchDatas)
        {
            foreach (var chunk in branchDatas.Chunk(100))
            {
                foreach (BranchData data in chunk)
                {
                    BranchNode node = _nodeFactory.CreateNode<BranchNode>(data.Position);

                    foreach (EventDataStringCondition item in data.EventData_StringConditions)
                    {
                        node.AddCondition(item);
                    }

                    node.LoadValueInToField();
                    node.ReloadLanguage();
                    graphView.AddElement(node);
                }
                await UniTask.Yield();
            }
        }

        async UniTask DrawChoiceNode(IEnumerable<ChoiceData> choiceDatas)
        {
            foreach (var chunk in choiceDatas.Chunk(100))
            {
                foreach (ChoiceData data in chunk)
                {
                    ChoiceNode node = _nodeFactory.CreateNode<ChoiceNode>(data.Position);

                    node.ChoiceData.ChoiceStateType.Value = data.ChoiceStateType.Value;

                    foreach (LanguageGeneric<string> dataText in data.Text)
                    {
                        foreach (LanguageGeneric<string> editorText in node.ChoiceData.Text)
                        {
                            if (editorText.LanguageType == dataText.LanguageType)
                            {
                                editorText.LanguageGenericType = dataText.LanguageGenericType;
                            }
                        }
                    }
                    foreach (LanguageGeneric<AudioClip> dataAudioClip in data.AudioClips)
                    {
                        foreach (LanguageGeneric<AudioClip> editorAudioClip in node.ChoiceData.AudioClips)
                        {
                            if (editorAudioClip.LanguageType == dataAudioClip.LanguageType)
                            {
                                editorAudioClip.LanguageGenericType = dataAudioClip.LanguageGenericType;
                            }
                        }
                    }

                    foreach (EventDataStringCondition item in data.EventData_StringConditions)
                    {
                        node.AddCondition(item);
                    }

                    node.LoadValueInToField();
                    node.ReloadLanguage();
                    graphView.AddElement(node);
                }
                await UniTask.Yield();
            }
        }

        async UniTask DrawDialogueNode(IEnumerable<DialogueData> dialogueDatas)
        {
            foreach (var chunk in dialogueDatas.Chunk(100))
            {
                foreach (DialogueData data in chunk)
                {
                    DialogueNode node = _nodeFactory.CreateNode<DialogueNode>(data.Position);

                    List<DialogueData_BaseContainer> data_BaseContainer = new List<DialogueData_BaseContainer>();

                    data_BaseContainer.AddRange(data.DialogueData_Imagess);
                    data_BaseContainer.AddRange(data.DialogueData_Texts);
                    data_BaseContainer.AddRange(data.DialogueData_Names);

                    data_BaseContainer.Sort(delegate (DialogueData_BaseContainer x, DialogueData_BaseContainer y)
                    {
                        return x.ID.Value.CompareTo(y.ID.Value);
                    });

                    foreach (DialogueData_BaseContainer item in data_BaseContainer)
                    {
                        switch (item)
                        {
                            case DialogueData_Name Name:
                                node.CharacterName(Name);
                                break;
                            case DialogueData_Text Text:
                                node.TextLine(Text);
                                break;
                            case DialogueData_Images image:
                                node.ImagePic(image);
                                break;
                            default:
                                break;
                        }
                    }

                    foreach (DialogueData_Port port in data.DialogueData_Ports)
                    {
                        node.AddChoicePort(node, port);
                    }

                    node.LoadValueInToField();
                    node.ReloadLanguage();
                    graphView.AddElement(node);

                }
                await UniTask.Yield();
            }
        }


        private void ConnectNodes(DialogueContainerSO dialogueContainer)
        {
            // Make connection for all node.
            for (int i = 0; i < nodes.Count; i++)
            {
                List<NodeLinkData> connections = dialogueContainer.NodeLinkDatas.Where(edge => edge.BaseNodeGuid == nodes[i].NodeId.ToString()).ToList();

                List<Port> allOutputPorts = nodes[i].outputContainer.Children().Where(x => x is Port).Cast<Port>().ToList();

                for (int j = 0; j < connections.Count; j++)
                {
                    BaseNode targetNode = nodes.First(node => node.NodeId.ToString() == connections[j].TargetNodeGuid);

                    if (targetNode == null) continue;

                    foreach (Port item in allOutputPorts)
                    {
                        if (item.portName == connections[j].BasePortName)
                        {
                            LinkNodesTogether(item, (Port)targetNode.inputContainer[0]);
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// Make connection for all node.
        ///// </summary>
        ///// <param name="dialogueContainer"></param>
        //private void ConnectNodes(DialogueContainerSO dialogueContainer)
        //{
        //    List<NodeLinkData> emptyLinks = new();
        //    List<Port> emptyPorts = new();
        //    Dictionary<string, List<NodeLinkData>> linkHash = dialogueContainer.NodeLinkDatas.GroupBy(e => e.BaseNodeGuid).ToDictionary(e => e.Key, e => e.ToList());
        //    Dictionary<string, BaseNode> nodeHash = nodes.ToDictionary(e => e.NodeGuid.ToString());
        //    foreach (var node in nodes)
        //    {
        //        List<NodeLinkData> connections = linkHash.GetValueOrDefault(node.NodeGuid.ToString(), emptyLinks);
        //        Dictionary<string, List<Port>> allOutputPortHash = node.outputContainer.Children().Where(x => x is Port).Cast<Port>().GroupBy(e => e.portName).ToDictionary(e => e.Key, e => e.ToList());
        //        foreach (var connection in connections)
        //        {
        //            BaseNode targetNode = nodeHash.GetValueOrDefault(connection.TargetNodeGuid, null);
        //            if (targetNode is null) continue;
        //            foreach (Port port in allOutputPortHash.GetValueOrDefault(connection.BasePortName, emptyPorts)) LinkNodesTogether(port, (Port)targetNode.inputContainer[0]);
        //        }
        //    }
        //}

        private void LinkNodesTogether(Port outputPort, Port inputPort)
        {
            Edge edge = new() { output = outputPort, input = inputPort };
            edge.input.Connect(edge);
            edge.output.Connect(edge);
            graphView.Add(edge);
        }

        #endregion
    }
}