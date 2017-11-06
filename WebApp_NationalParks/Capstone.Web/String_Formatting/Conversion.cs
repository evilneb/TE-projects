using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Capstone.Web.String_Formatting

{
    public class Conversion
    {
        public static string TempConversion(bool convertToCelsius, int tempFarenheit)
        {
            int temp = tempFarenheit;
            if (convertToCelsius)
            {

                temp = (int)(((1.0 * tempFarenheit) - 32) * 5.0/9.0);

            }
            string result = temp.ToString();
            if (convertToCelsius)
            {
                result += " C";
            }
            else
            {
                result += " F";
            }
            return result;
        }
    }
}