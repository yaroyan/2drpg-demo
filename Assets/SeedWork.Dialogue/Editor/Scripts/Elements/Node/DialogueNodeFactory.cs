using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    /// <summary>
    /// ダイアログ用のノードを生成するファクトリクラス
    /// </summary>
    internal class DialogueNodeFactory
    {
        readonly DialogueEditorWindow _editorWindow;
        readonly DialogueGraphView _graphView;
        readonly DynamicConstructor<BaseNode> _dynamicConstructor = new();

        public DialogueNodeFactory(DialogueEditorWindow editorWindow, DialogueGraphView graphView)
        {
            _editorWindow = editorWindow;
            _graphView = graphView;
        }

        /// <summary>
        /// GraphView上のマウスカーソルの座標を計算します。
        /// </summary>
        /// <param name="screenMousePosition">マウスカーソルの座標</param>
        /// <returns>マウスカーソルの座標</returns>
        public Vector2 CalculateMousePositionOnGraphView(Vector2 screenMousePosition) => _graphView.contentViewContainer.WorldToLocal(CalculateMousePositionOnWindow(screenMousePosition));

        /// <summary>
        /// スクリーン上のマウスカーソルの座標を計算します。
        /// </summary>
        /// <param name="screenMousePosition">マウスカーソルの座標</param>
        /// <returns>マウスカーソルの座標</returns>
        public Vector2 CalculateMousePositionOnWindow(Vector2 screenMousePosition) => _editorWindow.rootVisualElement.ChangeCoordinatesTo(_editorWindow.rootVisualElement.parent, screenMousePosition - _editorWindow.position.position);

        /// <summary>
        /// ノードを生成します。
        /// </summary>
        /// <param name="type">生成対象のノードの型</param>
        /// <param name="position">ノードを生成する座標</param>
        /// <returns>ノード</returns>
        public BaseNode CreateNode(Type type, Vector2 position)
        {
            var nodeId = Ulid.NewUlid();
            return _dynamicConstructor.Construct(type, new object[] { nodeId, position }) as BaseNode;
        }

        /// <summary>
        /// ノードを生成します。
        /// </summary>
        /// <typeparam name="T">生成対象のノードの型</typeparam>
        /// <param name="position">ノードを生成する座標</param>
        /// <returns>ノード</returns>
        public T CreateNode<T>(Vector2 position) where T : BaseNode
        {
            var nodeId = Ulid.NewUlid();
            return _dynamicConstructor.Construct<T>(new object[] { nodeId, position });
        }

        /// <summary>
        /// ノードを生成します。
        /// </summary>
        /// <typeparam name="T">生成対象のノードの型</typeparam>
        /// <typeparam name="U">生成対象のノードのデータの型</typeparam>
        /// <param name="nodeData">生成対象のノードのデータ</param>
        /// <returns>ノード</returns>
        public T CreateNode<T, U>(U nodeData) where T : BaseNode where U : BaseNodeData => _dynamicConstructor.Construct<T>(new object[] { nodeData });

    }
}
