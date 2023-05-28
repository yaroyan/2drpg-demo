using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SproutWork.Presentation.View
{
    /// <summary>
    /// 相互作用を及ぼすオブジェクトのインタフェース
    /// </summary>
    public interface IInteractable : IView
    {
        void Interact();
    }
}
