using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    [AttributeUsage(AttributeTargets.Property |
  AttributeTargets.Field, AllowMultiple = false)]
    sealed public class Custom3BarAttribute : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            bool result = true;
            var list = value as List<string>;
            if (list.Count == 0)
            {
                result = false;
            }
            else
            {
                foreach (string listItem in list)
                {
                    if (listItem == null || listItem.Length == 0)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }
    }
}