using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal class DictionaryNodeRepository<T> : IGenericNodeRepository<T> where T : BaseNode
    {
        private protected Dictionary<Ulid, T> _table;

        public DictionaryNodeRepository(IEnumerable<T> nodes)
        {
            _table = nodes.ToDictionary(e => e.NodeId);
        }

        public DictionaryNodeRepository()
        {
            _table = new();
        }

        public void Save(T node) => _table.Add(node.NodeId, node);
        public T FindById(Ulid id) => _table.GetValueOrDefault(id, default(T));
        public IReadOnlyCollection<T> FindAll(Func<T, bool> predicaate) => _table.Values.Where(predicaate).ToList();
        public IReadOnlyCollection<T> FindAll() => _table.Values.ToList();
        public T FirstOrDefault(Func<T, bool> predicate) => _table.Values.FirstOrDefault(predicate);
    }
}
