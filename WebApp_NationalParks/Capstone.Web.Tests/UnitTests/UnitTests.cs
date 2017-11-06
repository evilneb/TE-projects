using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.String_Formatting;

namespace Capstone.Web.Tests.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        //Test for TempConversion Method in Capstone.Web/String_Formatting/Conversion.cs
        [TestMethod]
        public void KeepInFarenheit()
        {
            int farenheit32 = 32;
            string result32F = Conversion.TempConversion(false, farenheit32);
            Assert.AreEqual("32 F", result32F);

            int farenheit100 = 100;
            string result100F = Conversion.TempConversion(false, farenheit100);
            Assert.AreEqual("100 F", result100F);

            int farenheit0 = 0;
            string result0F = Conversion.TempConversion(false, farenheit0);
            Assert.AreEqual("0 F", result0F);
        }

        //Test for TempConversion Method in Capstone.Web/String_Formatting/Conversion.cs
        [TestMethod]
        public void Convert2Celsius()
        {
            int farenheit32 = 32;
            string result32FToC = Conversion.TempConversion(true, farenheit32);
            Assert.AreEqual("0 C", result32FToC);

            int farenheit212 = 212;
            string result100FToC = Conversion.TempConversion(true, farenheit212);
            Assert.AreEqual("100 C", result100FToC);

            int farenheitNeg = -10;
            string resultNeg23FToC = Conversion.TempConversion(true, farenheitNeg);
            Assert.AreEqual("-23 C", resultNeg23FToC);

            int farenheit0 = 0;
            string result0FToC = Conversion.TempConversion(true, farenheit0);
            Assert.AreEqual("-17 C", result0FToC);
        }

        //Test for CommaInteger Method in Capstone.Web/String_Formatting/Formatting.cs
        [TestMethod]
        public void CommaFormat()
        {
            string num1 = "1";
            string num1result = Formatting.CommaInteger(num1);
            Assert.AreEqual("1", num1result);

            string num2 = "12";
            string num2result = Formatting.CommaInteger(num2);
            Assert.AreEqual("12", num2result);

            string num3 = "123";
            string num3result = Formatting.CommaInteger(num3);
            Assert.AreEqual("123", num3result);

            string num4 = "1234";
            string num4result = Formatting.CommaInteger(num4);
            Assert.AreEqual("1,234", num4result);

            string num5 = "12345";
            string num5result = Formatting.CommaInteger(num5);
            Assert.AreEqual("12,345", num5result);

            string num6 = "123456";
            string num6result = Formatting.CommaInteger(num6);
            Assert.AreEqual("123,456", num6result);

            string num7 = "1234567";
            string num7result = Formatting.CommaInteger(num7);
            Assert.AreEqual("1,234,567", num7result);

            string num8 = "123456789123456";
            string num8result = Formatting.CommaInteger(num8);
            Assert.AreEqual("123,456,789,123,456", num8result);
        }

        //Test for Money Method in Capstone.Web/String_Formatting/Formatting.cs
        [TestMethod]
        public void MoneyFormat()
        {
            string num1 = "1";
            string num1result = Formatting.Money(num1);
            Assert.AreEqual("$1.00", num1result);

            string num2 = "12.345";
            string num2result = Formatting.Money(num2);
            Assert.AreEqual("$12.35", num2result);

            string num3 = "0";
            string num3result = Formatting.Money(num3);
            Assert.AreEqual("$0.00", num3result);
        }
    }
}
