using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yaroyan.SeedWork.Common.JSON
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}
