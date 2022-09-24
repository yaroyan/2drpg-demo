using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace Yaroyan.SeedWork.Common.JSON
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public T Deserialize<T>(Type type, string json) => (T)JsonConvert.DeserializeObject(json, type);
    }
}
