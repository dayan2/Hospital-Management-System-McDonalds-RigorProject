#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
#endregion

namespace Mcd.HospitalManagement.Web.Enums
{
    public static class CustomEnumMessage
    {
        #region Public methods for get string type enum messages
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType(); //Get type of enum value
            FieldInfo fieldInfo = type.GetField(value.ToString());  //Get field info related enum value
            //Put customize enum attribute to EnumStringAttribute array
            EnumStringAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(EnumStringAttribute), false) as EnumStringAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null; //Return string value zero index value if attribs array is not null
        }
        #endregion


    }
}