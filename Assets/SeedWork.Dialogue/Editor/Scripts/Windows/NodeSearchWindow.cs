using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        DialogueGraphView _graphView;
        DialogueNodeFactory _nodeFactory;
        Texture2D _iconImage;

        public void Configure(DialogueGraphView graphView, DialogueNodeFactory nodeFactory)
        {
            _graphView = graphView;
            _nodeFactory = nodeFactory;
            // Icon image that we kinda don't use.
            // However use it to create space left of the text.
            // TODO: find a better way.
            _iconImage = new Texture2D(1, 1);
            _iconImage.SetPixel(0, 0, new Color(0, 0, 0, 0));
            _iconImage.Apply();
        }


        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Dialogue Editor"),0),
            new SearchTreeGroupEntry(new GUIContent("Dialogue Node"),1),
        };
            var entries = typeof(BaseNode).Assembly.GetTypes()
                .Where(e => e.IsSubclassOf(typeof(BaseNode)) && !e.IsAbstract)
                .Select(e => new SearchTreeEntry(new GUIContent(e.Name, _iconImage)) { level = 2, userData = e });
            foreach (var entry in entries) tree.Add(entry);
            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var node = _nodeFactory.CreateNode(searchTreeEntry.userData as Type, _nodeFactory.CalculateMousePositionOnGraphView(context.screenMousePosition));
            if (node is null) return false;
            _graphView.AddElement(node);
            node.MutateOnLoad();
            return true;
        }
    }

}