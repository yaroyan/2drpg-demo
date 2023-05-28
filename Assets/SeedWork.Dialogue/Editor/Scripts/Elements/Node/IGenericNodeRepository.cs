using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal interface IGenericNodeRepository<T> : INodeRepository where T : BaseNode
    {
        public T FindById(Ulid id);
        public IReadOnlyCollection<T> FindAll(Func<T, bool> predicaate);
        public IReadOnlyCollection<T> FindAll();
        public T FirstOrDefault(Func<T, bool> predicate);
    }
}
