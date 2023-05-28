using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal abstract class NodeErrorMessage : ErrorMessage
    {
        internal NodeErrorMessage(int id, string name, string message) : base(id, name, message)
        {
        }
    }

    [Serializable]
    internal class ChoiceNodeErrorMessage : NodeErrorMessage
    {
        internal static ChoiceNodeErrorMessage Error001 = new(1, nameof(Error001), "入力ポートにノードが未接続です。");
        internal static ChoiceNodeErrorMessage Error002 = new(1, nameof(Error002), "出力ポートにノードが未接続です。");
        internal ChoiceNodeErrorMessage(int id, string name, string message) : base(id, name, message)
        {
        }
    }

    [Serializable]
    internal class EndNodeErrorMessage : NodeErrorMessage
    {
        internal static EndNodeErrorMessage Error001 = new(1, nameof(Error001), "入力ポートにノードが未接続です。");

        internal EndNodeErrorMessage(int id, string name, string message) : base(id, name, message)
        {
        }
    }

    [Serializable]
    internal class StartNodeErrorMessage : NodeErrorMessage
    {
        internal static StartNodeErrorMessage Error001 = new(1, nameof(Error001), "出力ポートにノードが未接続です。");

        internal StartNodeErrorMessage(int id, string name, string message) : base(id, name, message)
        {
        }
    }

    [Serializable]
    internal class EventNodeErrorMessage : NodeErrorMessage
    {
        internal static EventNodeErrorMessage Error001 = new(1, nameof(Error001), "入力ポートにノードが未接続です。");
        internal static EventNodeErrorMessage Error002 = new(1, nameof(Error002), "出力ポートにノードが未接続です。");
        internal EventNodeErrorMessage(int id, string name, string message) : base(id, name, message)
        {
        }
    }

    [Serializable]
    internal class DialogueNodeErrorMessage : NodeErrorMessage
    {
        internal static DialogueNodeErrorMessage Error001 = new(1, nameof(Error001), "入力ポートにノードが未接続です。");
        internal static DialogueNodeErrorMessage Error002 = new(1, nameof(Error002), "出力ポートにノードが未接続です。");
        internal DialogueNodeErrorMessage(int id, string name, string message) : base(id, name, message)
        {
        }
    }

    [Serializable]
    internal class BranchNodeErrorMessage : NodeErrorMessage
    {
        internal static BranchNodeErrorMessage Error001 = new(1, nameof(Error001), "入力ポートにノードが未接続です。");
        internal static BranchNodeErrorMessage Error002 = new(1, nameof(Error002), "条件が真の場合の出力ポートにノードが未接続です。");
        internal static BranchNodeErrorMessage Error003 = new(1, nameof(Error003), "条件が偽の場合の出力ポートにノードが未接続です。");
        internal BranchNodeErrorMessage(int id, string name, string message) : base(id, name, message)
        {
        }
    }
}
