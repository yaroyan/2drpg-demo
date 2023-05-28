using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.Common.JSON
{
    public interface IJsonSerializer
    {
        public string Serialize(object obj);
        public string Serialize<T>(T obj);
    }
}
