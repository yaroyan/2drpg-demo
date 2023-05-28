using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class ChoiceNode : GenericBaseNode<ChoiceData>
    {
        public ChoiceData ChoiceData { get; private set; }

        Box choiceStateEnumBox;

        Port inputPort;
        Port outputPort;

        public ChoiceNode(NodeData<ChoiceData> nodeData) : base(nodeData)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/ChoiceNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Choice";
            ChoiceData = nodeData.Data;
            inputPort = AddInputPort("Input", Port.Capacity.Multi);
            outputPort = AddOutputPort("Output", Port.Capacity.Single);
            inputPort.portColor = Color.yellow;
        }

        public ChoiceNode(Ulid nodeId, Vector2 position) : base(nodeId, position)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/ChoiceNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Choice";
            ChoiceData = new ChoiceData(nodeId);
            inputPort = AddInputPort("Input", Port.Capacity.Multi);
            outputPort = AddOutputPort("Output", Port.Capacity.Single);
            inputPort.portColor = Color.yellow;
        }

        public override void MutateOnLoad()
        {
            TopButton();
            TextLine();
            ChoiceStateEnum();
        }

        public override NodeErrorInfo InspectConsistency()
        {
            NodeErrorInfo info = new NodeErrorInfo(NodeId);
            if (!inputPort.connected) info.AddError(new NodeError(ChoiceNodeErrorMessage.Error001, () => inputPort.connected));
            if (!outputPort.connected) info.AddError(new NodeError(ChoiceNodeErrorMessage.Error002, () => outputPort.connected));
            return info;
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
            AddStringConditionEventBuild(ChoiceData.EventData_StringConditions, stringEvent);
            ShowHideChoiceEnum();
        }

        public void TextLine()
        {
            // Make Container Box
            Box boxContainer = new Box();
            boxContainer.AddToClassList("TextLineBox");

            // Text
            TextField textField = GetNewTextField_TextLanguage(ChoiceData.Text, "Text", "TextBox");
            ChoiceData.TextField = textField;
            boxContainer.Add(textField);

            // Audio
            ObjectField objectField = GetNewObjectField_AudioClipsLanguage(ChoiceData.AudioClips, "AudioClip");
            ChoiceData.ObjectField = objectField;
            boxContainer.Add(objectField);

            // Reaload the current selected language
            ReloadLanguage();

            mainContainer.Add(boxContainer);
        }

        private void ChoiceStateEnum()
        {
            choiceStateEnumBox = new Box();
            choiceStateEnumBox.AddToClassList("BoxRow");
            ShowHideChoiceEnum();

            // Make fields.
            Label enumLabel = GetNewLabel("If the condition is not met", "ChoiceLabel");
            EnumField choiceStateEnumField = GetNewEnumField_ChoiceStateType(ChoiceData.ChoiceStateType, "enumHide");

            // Add fields to box.
            choiceStateEnumBox.Add(choiceStateEnumField);
            choiceStateEnumBox.Add(enumLabel);

            mainContainer.Add(choiceStateEnumBox);
        }

        protected override void DeleteBox(Box boxContainer)
        {
            base.DeleteBox(boxContainer);
            ShowHideChoiceEnum();
        }

        private void ShowHideChoiceEnum()
        {
            ShowContainer(ChoiceData.EventData_StringConditions.Count > 0, choiceStateEnumBox);
        }

        public override void LoadValueInToField()
        {
            //if (ChoiceData.ChoiceStateType.EnumField != null)
            //    ChoiceData.ChoiceStateType.EnumField.SetValueWithoutNotify(ChoiceData.ChoiceStateType.Value);
        }

        public override NodeData<ChoiceData> Export()
        {
            throw new NotImplementedException();
        }
    }
}