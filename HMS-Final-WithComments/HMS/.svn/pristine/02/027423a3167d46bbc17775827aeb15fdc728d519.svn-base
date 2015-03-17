using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Mcd.HospitalManagement.Web.Enums
{
    public static class CustomEnumMessage
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            EnumStringAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(EnumStringAttribute), false) as EnumStringAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
        
        
    }
}