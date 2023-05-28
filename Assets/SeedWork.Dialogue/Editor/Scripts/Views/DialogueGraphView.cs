using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class DialogueGraphView : GraphView
    {
        // Name of the graph view style sheet.
        readonly string graphViewStyleSheet = "USS/GraphView/GraphViewStyleSheet";
        DialogueEditorWindow _editorWindow;
        MiniMap _miniMap;
        public DialogueNodeFactory NodeFactory { get; }
        public LanguageType Language { get => _editorWindow.SelectedLanguage; }

        public DialogueGraphView(DialogueEditorWindow editorWindow)
        {
            this._editorWindow = editorWindow;
            NodeFactory = new DialogueNodeFactory(_editorWindow, this);

            // Adding the ability to zoom in and out graph view.
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            // Adding style sheet to graph view.
            styleSheets.Add(Resources.Load<StyleSheet>(graphViewStyleSheet));

            // The ability to drag nodes around.
            this.AddManipulator(new ContentDragger());
            // The ability to drag all selected nodes around.
            this.AddManipulator(new SelectionDragger());
            // The ability to drag select a rectangle area.
            this.AddManipulator(new RectangleSelector());
            // The ability to select a single node.
            this.AddManipulator(new FreehandSelector());
            AddMiniMap();
            AddGridBackground();
            nodeCreationRequest += OnNodeCreationRequest;
        }

        void OnNodeCreationRequest(NodeCreationContext context)
        {
            NodeSearchWindow searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            searchWindow.Configure(this, NodeFactory);
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        void AddMiniMap()
        {
            _miniMap = new MiniMap();
            _miniMap.SetPosition(new Rect(15, 50, 200, 180));
            Add(_miniMap);
        }

        void AddGridBackground()
        {
            // Add a visible grid to the background.
            GridBackground grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }

        public void ToggleVisibilityForMinimap() => _miniMap.visible = !_miniMap.visible;

        // This is a graph view method that we override.
        // This is where we tell the graph view which nodes can connect to each other.
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            // All the ports that can be connected to.
            List<Port> compatiblePorts = new List<Port>();
            // Start port.
            Port startPortView = startPort;

            ports.ForEach((port) =>
            {
                Port portView = port;

                // First we tell that it cannot connect to itself.
                // Then we tell it it cannot connect to a port on the same node.
                // Lastly we tell it a input note cannot connect to another input node and an output node cannot connect to output node.
                if (startPortView != portView && startPortView.node != portView.node && startPortView.direction != port.direction && startPortView.portColor == portView.portColor)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts; // return all the acceptable ports.
        }

        /// <summary>
        /// Reload the current selected language.
        /// Normally used when changing language.
        /// </summary>
        public void ReloadLanguage()
        {
            List<BaseNode> allNodes = nodes.ToList().Where(node => node is BaseNode).Cast<BaseNode>().ToList();
            foreach (BaseNode node in allNodes)
            {
                node.ReloadLanguage();
            }
        }
    }
}