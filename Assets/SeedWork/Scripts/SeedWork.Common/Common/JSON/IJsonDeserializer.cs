using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.Common.JSON
{
    public interface IJsonDeserializer
    {
        T Deserialize<T>(string json);
        T Deserialize<T>(Type type, string json);
    }
}
