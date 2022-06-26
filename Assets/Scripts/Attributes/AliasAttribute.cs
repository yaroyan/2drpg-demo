using System.Collections;
using System.Collections.Generic;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
public class AliasAttribute : System.Attribute
{
    public string Alias { get; private set; }
    public AliasAttribute(string alias)
    {
        this.Alias = alias;
    }
}
