using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.Common.Extension.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class AliasAttribute : System.Attribute
    {
        public string Alias { get; private set; }
        public AliasAttribute(string alias)
        {
            this.Alias = alias;
        }
    }
}

