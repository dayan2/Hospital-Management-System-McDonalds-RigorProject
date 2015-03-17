using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Mcd.HospitalManagement.Web.Enums
{
    public class EnumStringAttribute : Attribute
    {
        public EnumStringAttribute(string stringValue)
        {
            this.stringValue = stringValue;
        }
        private string stringValue;
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        
    }
}