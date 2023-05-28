using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    /// <summary>
    /// ノードのデザインを定義するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Dialogue Editor/New Node Design")]
    internal class NodeDesign : ScriptableObject
    {
        static Vector2 s_defaultSize = new Vector2(200, 200);
        [field: SerializeField]
        internal Vector2 DefaultSize { get; set; } = new Vector2(200, 250);
        [field: SerializeField]
        internal Color Color { get; set; }
        [field: SerializeField]
        internal StyleSheet StyleSheet { get; set; }

        void OnValidate()
        {
            NormalizeDefaultSize();
        }

        void NormalizeDefaultSize()
        {
            if (DefaultSize.x < s_defaultSize.x)
            {
                DefaultSize = new Vector2(s_defaultSize.x, DefaultSize.y);
                if (DefaultSize.y < s_defaultSize.y)
                {
                    DefaultSize = s_defaultSize;
                }
            }
            if (DefaultSize.y < s_defaultSize.y)
            {
                DefaultSize = new Vector2(DefaultSize.x, s_defaultSize.y);
                if (DefaultSize.x < s_defaultSize.x)
                {
                    DefaultSize = s_defaultSize;
                }
            }
        }
    }
}
