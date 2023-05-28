using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class StartNode : GenericBaseNode<StartData>
    {
        Port outputPort;

        public StartNode(NodeData<StartData> nodeData) : base(nodeData)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/StartNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Start";
            outputPort = AddOutputPort("Output", Port.Capacity.Single);
            RefreshExpandedState();
            RefreshPorts();
        }

        public StartNode(Ulid nodeId, Vector2 position) : base(nodeId, position)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/StartNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Start";
            outputPort = AddOutputPort("Output", Port.Capacity.Single);
            RefreshExpandedState();
            RefreshPorts();
        }

        public override void MutateOnLoad()
        {

        }

        public override NodeData<StartData> Export()
        {
            return new NodeData<StartData>(ExportMetadata(), new StartData(NodeId));
        }

        public override NodeErrorInfo InspectConsistency()
        {
            NodeErrorInfo info = new NodeErrorInfo(NodeId);
            if (!outputPort.connected) info.AddError(new NodeError(StartNodeErrorMessage.Error001, () => outputPort.connected));
            return info;
        }
    }
}