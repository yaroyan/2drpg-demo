using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class EndNode : GenericBaseNode<EndData>
    {
        Port inputPort;

        public EndData EndData { get; private set; }

        public EndNode(NodeData<EndData> nodeData) : base(nodeData)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/EndNodeStyleSheet");
            styleSheets.Add(styleSheet);
            EndData = nodeData.Data;
            title = "End";
            inputPort = AddInputPort("Input", Port.Capacity.Multi);
        }

        public EndNode(Ulid nodeId, Vector2 position) : base(nodeId, position)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/EndNodeStyleSheet");
            styleSheets.Add(styleSheet);
            EndData = new EndData(nodeId);
            title = "End";
            inputPort = AddInputPort("Input", Port.Capacity.Multi);
        }

        public override void MutateOnLoad()
        {
            MakeMainContainer();
        }

        public override NodeData<EndData> Export()
        {
            return new NodeData<EndData>(ExportMetadata(), new EndData(NodeId));
        }

        public override NodeErrorInfo InspectConsistency()
        {
            NodeErrorInfo info = new NodeErrorInfo(NodeId);
            if (!inputPort.connected) info.AddError(new NodeError(EndNodeErrorMessage.Error001, () => inputPort.connected));
            return info;
        }

        private void MakeMainContainer()
        {
            EnumField enumField = GetNewEnumField_EndNodeType(EndData.EndNodeType);

            mainContainer.Add(enumField);
        }

        public override void LoadValueInToField()
        {
            //if (EndData.EndNodeType.EnumField is not null)
            //    EndData.EndNodeType.EnumField.SetValueWithoutNotify(EndData.EndNodeType.Value);
        }
    }
}