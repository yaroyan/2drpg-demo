using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.Common.Extension.Attribute;

namespace Yaroyan.SeedWork.Common.Extension
{
    public static class EnumExtension
    {
        public static string GetAlias(this System.Enum value)
        {
            {
                System.Type type = value.GetType();
                System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());
                if (fieldInfo == null) return null;
                AliasAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(AliasAttribute), false) as AliasAttribute[];
                return attrs.Length > 0 ? attrs[0].Alias : null;
            }
        }
    }
}