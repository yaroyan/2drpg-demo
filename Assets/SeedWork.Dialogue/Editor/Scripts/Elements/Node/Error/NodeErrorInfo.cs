using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    [Serializable]
    internal class NodeErrorInfo
    {
        internal readonly string NodeId;
        List<NodeError> _errors = new();

        internal IReadOnlyCollection<NodeError> NodeErrors { get => _errors; }

        internal NodeErrorInfo(Ulid nodeId) => (NodeId) = (nodeId.ToString());

        public void AddError(NodeError error) => _errors.Add(error);
    }
}
