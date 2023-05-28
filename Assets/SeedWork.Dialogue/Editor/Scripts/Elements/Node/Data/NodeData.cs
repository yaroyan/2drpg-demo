using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal abstract class BaseNodeData
    {
        NodeMetadata _metadata;
        internal NodeMetadata Metadata { get => _metadata; private protected set => _metadata = value ?? throw new ArgumentException(nameof(Metadata)); }
    }

    internal class NodeData<T> : BaseNodeData where T : BaseData
    {
        T _data;
        internal T Data { get => _data; private set => _data = value ?? throw new ArgumentException(nameof(Data)); }

        internal NodeData(NodeData<T> nodeData)
        {
            Data = nodeData.Data;
            Metadata = nodeData.Metadata;
        }

        internal NodeData(NodeMetadata metadata, T data)
        {
            Metadata = metadata;
            Data = data;
        }
    }
}
