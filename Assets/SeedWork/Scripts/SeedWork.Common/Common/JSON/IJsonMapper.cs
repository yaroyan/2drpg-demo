using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.Common.JSON
{
    public interface IJsonMapper : IJsonSerializer, IJsonDeserializer
    {
        public T DeepCopy<T>(T obj);
    }
}
