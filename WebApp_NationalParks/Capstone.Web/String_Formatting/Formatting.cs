using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.String_Formatting
{
    public class Formatting
    {
        public static string CommaInteger(string x)
        {
            string result = x;
            int numCommas = result.Length / 3;
            int remainder = result.Length % 3;
            for (int i = remainder + ((numCommas - 1) * 3); i > 0; i = i - 3)
            {
                result = result.Insert(i, ",");
            }
            return result;
        }

        public static string Money(string x)
        {
            double money = Convert.ToDouble(x);
            return money.ToString("C2");
        }
    }
}