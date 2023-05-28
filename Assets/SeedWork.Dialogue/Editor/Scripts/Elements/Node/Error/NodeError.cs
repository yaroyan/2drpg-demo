using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class NodeError
    {
        public readonly NodeErrorMessage Message;
        public readonly Func<bool> Validator;

        internal NodeError(NodeErrorMessage message, Func<bool> validator) => (Message, Validator) = (message, validator);
    }
}
