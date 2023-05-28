using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    /// <summary>
    /// ノードのメタデータ
    /// エディタ拡張のGraphViewで描画するためのメタデータ
    /// </summary>
    public class NodeMetadata
    {
        /// <summary>
        /// 一意識別子
        /// </summary>
        public Ulid NodeId { get; private set; }
        /// <summary>
        /// 位置情報
        /// </summary>
        internal Vector2 NodePosition { get; set; }
        /// <summary>
        /// サイズ
        /// </summary>
        internal Vector2 NodeSize { get; set; }

        public NodeMetadata(Ulid id)
        {
            NodeId = id;
        }
    }
}
