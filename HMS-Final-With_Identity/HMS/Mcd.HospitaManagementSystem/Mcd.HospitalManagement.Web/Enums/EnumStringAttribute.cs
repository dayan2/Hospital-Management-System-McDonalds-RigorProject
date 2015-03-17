﻿#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
#endregion

namespace Mcd.HospitalManagement.Web.Enums
{
    public class EnumStringAttribute : Attribute
    {
        #region Public constructor for get enum string attibute
        public EnumStringAttribute(string stringValue)
        {
            this.stringValue = stringValue;
        }
        #endregion

        #region public property for get string value 
        private string stringValue;
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
        #endregion


    }
}