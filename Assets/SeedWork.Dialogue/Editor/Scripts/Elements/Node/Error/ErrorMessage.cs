using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal abstract class ErrorMessage : Enumeration
    {
        internal string Message { get; private set; }
        internal ErrorMessage(int id, string name, string message) : base(id, name)
        {
            Message = message;
        }

        internal string ComposeMessage(params object[] args)
        {
            if (args.Length == 0) return Message;
            return string.Format(Message, args);
        }
    }
}
