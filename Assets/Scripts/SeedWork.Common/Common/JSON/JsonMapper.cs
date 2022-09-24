using System;
using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.Common.JSON
{
    public class JsonMapper : IJsonMapper
    {
        readonly IJsonSerializer _serializer;
        readonly IJsonDeserializer _deserializer;

        public JsonMapper(IJsonSerializer serializer, IJsonDeserializer deserializer)
        {
            _serializer = serializer;
            _deserializer = deserializer;
        }

        public T DeepCopy<T>(T obj) => _deserializer.Deserialize<T>(_serializer.Serialize(obj));

        public T Deserialize<T>(string json) => _deserializer.Deserialize<T>(json);

        public T Deserialize<T>(Type type, string json) => _deserializer.Deserialize<T>(type, json);

        public string Serialize(object obj) => _serializer.Serialize(obj);

        public string Serialize<T>(T obj) => _serializer.Serialize(obj);
    }
}
