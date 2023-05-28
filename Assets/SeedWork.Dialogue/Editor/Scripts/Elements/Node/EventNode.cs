using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class EventNode : GenericBaseNode<EventData>
    {
        Port outputPort;
        Port inputPort;

        public EventData EventData { get; private set; }

        public EventNode(NodeData<EventData> nodeData) : base(nodeData)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/EventNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Event";
            EventData = nodeData.Data;
            inputPort = AddInputPort("Input", Port.Capacity.Multi);
            outputPort = AddOutputPort("Output", Port.Capacity.Single);
        }

        public EventNode(Ulid nodeId, Vector2 position) : base(nodeId, position)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/EventNodeStyleSheet");
            styleSheets.Add(styleSheet);
            title = "Event";
            EventData = new EventData(nodeId);
            inputPort = AddInputPort("Input", Port.Capacity.Multi);
            outputPort = AddOutputPort("Output", Port.Capacity.Single);
        }

        public override void MutateOnLoad()
        {
            TopButton();
        }

        public override NodeData<EventData> Export()
        {
            throw new NotImplementedException();
        }

        public override NodeErrorInfo InspectConsistency()
        {
            NodeErrorInfo info = new NodeErrorInfo(NodeId);
            if (!inputPort.connected) info.AddError(new NodeError(EventNodeErrorMessage.Error001, () => inputPort.connected));
            if (!outputPort.connected) info.AddError(new NodeError(EventNodeErrorMessage.Error002, () => outputPort.connected));
            return info;
        }

        private void TopButton()
        {
            ToolbarMenu menu = new ToolbarMenu();
            menu.text = "Add Event";

            menu.menu.AppendAction("String Event Modifier", new Action<DropdownMenuAction>(x => AddStringEvent()));
            menu.menu.AppendAction("Scriptable Object", new Action<DropdownMenuAction>(x => AddScriptableEvent()));

            titleContainer.Add(menu);
        }

        public void AddStringEvent(EventDataStringModifier stringEvent = null)
        {
            AddStringModifierEventBuild(EventData.EventDataStringModifiers, stringEvent);
        }

        public void AddScriptableEvent(Ref<DialogueEventSO> paramidaEventScriptableObjectData = null)
        {
            Ref<DialogueEventSO> tmpDialogueEventSO = new();

            // If we paramida value is not null we load in values.
            if (paramidaEventScriptableObjectData != null)
            {
                tmpDialogueEventSO.Value = paramidaEventScriptableObjectData.Value;
            }
            EventData.DialogueEventSOs.Add(tmpDialogueEventSO);

            // Container of all object.
            Box boxContainer = new Box();
            boxContainer.AddToClassList("EventBox");

            // Scriptable Object Event.
            ObjectField objectField = CreateObjectField(tmpDialogueEventSO, "EventObject");

            // Remove button.
            Button btn = GetNewButton("X", "removeBtn");
            btn.clicked += () =>
            {
                DeleteBox(boxContainer);
                EventData.DialogueEventSOs.Remove(tmpDialogueEventSO);
            };

            // Add it to the box
            boxContainer.Add(objectField);
            boxContainer.Add(btn);

            mainContainer.Add(boxContainer);
            RefreshExpandedState();
        }
    }
}