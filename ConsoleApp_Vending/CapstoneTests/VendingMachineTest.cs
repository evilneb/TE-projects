using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest
    {
        VendingMachine vendTest = new VendingMachine();

        [TestMethod]
        public void FeedMoneyTest()
        {
            vendTest.FeedMoney(4);
            Assert.AreEqual(4.00, vendTest.UserMoney);
        }

        [TestMethod]
        public void SelectProductTest()
        {
            Assert.AreEqual("Bad item code! Please try again.", vendTest.SelectProduct("F6"));

            vendTest.UserMoney = 0;
            Assert.AreEqual("Not enough money! Please insert more money.", vendTest.SelectProduct("D1"));

            vendTest.UserMoney = 5.00;
            vendTest.Inventory["A1"].Quantity = 0;
            Assert.AreEqual("That item is gone! Please select a different item.", vendTest.SelectProduct("A1"));

            vendTest.UserMoney = 10.00;
            Assert.AreEqual("Munch Munch, Yum!", vendTest.SelectProduct("B3"));
            Assert.AreEqual(8.50, vendTest.UserMoney);
            Assert.AreEqual(1.50, vendTest.Bank);
            Assert.AreEqual(4, vendTest.Inventory["B3"].Quantity);

            Assert.AreEqual("Crunch Crunch, Yum!", vendTest.SelectProduct("A2"));
            Assert.AreEqual(7.05, vendTest.UserMoney);
            Assert.AreEqual(2.95, vendTest.Bank);
            Assert.AreEqual(4, vendTest.Inventory["A2"].Quantity);

            Assert.AreEqual("Glug Glug, Yum!", vendTest.SelectProduct("C1"));
            Assert.AreEqual(5.80, vendTest.UserMoney);
            Assert.AreEqual(4.20, vendTest.Bank);
            Assert.AreEqual(4, vendTest.Inventory["C1"].Quantity);

            Assert.AreEqual("Chew Chew, Yum!", vendTest.SelectProduct("D4"));
            Assert.AreEqual(5.05, vendTest.UserMoney);
            Assert.AreEqual(4.95, vendTest.Bank);
            Assert.AreEqual(4, vendTest.Inventory["D4"].Quantity);
        }

        [TestMethod]
        public void MakeChangeTest()
        {
            vendTest.UserMoney = 4.45;
            Assert.AreEqual("17 Quarters 2 Dimes returned.", vendTest.MakeChange());

            vendTest.UserMoney = 0.30;
            Assert.AreEqual("1 Quarter 1 Nickel returned.", vendTest.MakeChange());

            vendTest.UserMoney = 0.15;
            Assert.AreEqual("1 Dime 1 Nickel returned.", vendTest.MakeChange());

            vendTest.UserMoney = 0.40;
            Assert.AreEqual("1 Quarter 1 Dime 1 Nickel returned.", vendTest.MakeChange());
        }
    }
}
