using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTest
    {
        [TestMethod]
        public void ConsumeItemTest()
        {
            VendingMachine vendTest = new VendingMachine();

            vendTest.Inventory["A3"].ConsumeItem();
            Assert.AreEqual(4, vendTest.Inventory["A3"].Quantity);

            vendTest.Inventory["A3"].ConsumeItem();
            Assert.AreEqual(3, vendTest.Inventory["A3"].Quantity);

            vendTest.Inventory["B4"].ConsumeItem();
            vendTest.Inventory["B4"].ConsumeItem();
            vendTest.Inventory["B4"].ConsumeItem();
            Assert.AreEqual(2, vendTest.Inventory["B4"].Quantity);

            vendTest.Inventory["D2"].ConsumeItem();
            vendTest.Inventory["D2"].ConsumeItem();
            vendTest.Inventory["D2"].ConsumeItem();
            vendTest.Inventory["D2"].ConsumeItem();
            vendTest.Inventory["D2"].ConsumeItem();
            Assert.AreEqual(0, vendTest.Inventory["D2"].Quantity);
        }
    }
}
