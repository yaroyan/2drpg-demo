using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class BranchNode : GenericBaseNode<BranchData>
    {
        private readonly Port _trueOutputPort;
        private readonly Port _falseOutputPort;
        private readonly Port _inputPort;

        public BranchData BranchData { get; private set; }

        public BranchNode(NodeData<BranchData> nodeData) : base(nodeData)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/BranchNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Branch";
            BranchData = nodeData.Data;
            _inputPort = AddInputPort("Input", Port.Capacity.Multi);
            _trueOutputPort = AddOutputPort("True", Port.Capacity.Single);
            _falseOutputPort = AddOutputPort("False", Port.Capacity.Single);
        }

        public BranchNode(Ulid nodeId, Vector2 position) : base(nodeId, position)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/BranchNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Branch";
            BranchData = new BranchData(nodeId);
            _inputPort = AddInputPort("Input", Port.Capacity.Multi);
            _trueOutputPort = AddOutputPort("True", Port.Capacity.Single);
            _falseOutputPort = AddOutputPort("False", Port.Capacity.Single);
        }

        public override void MutateOnLoad()
        {
            TopButton();
        }

        private void TopButton()
        {
            ToolbarMenu Menu = new ToolbarMenu();
            Menu.text = "Add Condition";

            Menu.menu.AppendAction("String Event Condition", new Action<DropdownMenuAction>(x => AddCondition()));

            titleButtonContainer.Add(Menu);
        }

        public void AddCondition(EventDataStringCondition stringEvent = null)
        {
            AddStringConditionEventBuild(BranchData.EventData_StringConditions, stringEvent);
        }

        public override NodeErrorInfo InspectConsistency()
        {
            NodeErrorInfo info = new NodeErrorInfo(NodeId);
            if (!_inputPort.connected) info.AddError(new NodeError(BranchNodeErrorMessage.Error001, () => _inputPort.connected));
            if (!_trueOutputPort.connected) info.AddError(new NodeError(BranchNodeErrorMessage.Error002, () => _trueOutputPort.connected));
            if (!_falseOutputPort.connected) info.AddError(new NodeError(BranchNodeErrorMessage.Error003, () => _falseOutputPort.connected));
            return info;
        }

        public override NodeData<BranchData> Export()
        {
            throw new NotImplementedException();
        }
    }
}
