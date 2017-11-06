using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public double Bank { get; set; }
        public double UserMoney { get; set; }
        public Dictionary<string, Item> Inventory { get; }
        public Dictionary<string, int> SalesCount { get; set; }

        public VendingMachine()
        {
            UserMoney = 0;
            Inventory = ReadWriteData.ReadInventory();
            SalesCount = ReadWriteData.ReadSales();
            Bank = Convert.ToDouble(SalesCount["Bank"]/100);
        }

        public List<string> PrintInventory()
        {
            List<string> table = new List<string>();

            int slotPad = 10;
            int itemPad = 20;
            int pricePad = 10;

            string header = "Slot".PadRight(slotPad) + "Item".PadRight(itemPad) + "Price".PadRight(pricePad) + "Quantity";
            table.Add(header);

            string headerLine = "";
            foreach (char c in table[0])
            {
                headerLine += "-";
            }
            table.Add(headerLine);

            foreach (KeyValuePair<string, Item> item in Inventory)
            {

                string printString = item.Key.PadRight(slotPad) + item.Value.Name.PadRight(itemPad) + Menu.ToDollars(item.Value.Price).PadRight(pricePad);

                if (item.Value.Quantity == 0)
                {
                    printString += "SOLD OUT";
                }
                else
                {
                    printString += item.Value.Quantity;
                }
                
                table.Add(printString);
            }
            return table;
        }
        public void FeedMoney(int input)
        {
            double startLog = input;

            UserMoney += input;

            double endLog = UserMoney;
            ReadWriteData.WriteLog("FEED MONEY:", startLog, endLog);
        }
        public string SelectProduct(string input)
        {
            if (!Inventory.ContainsKey(input))
                return "Bad item code! Please try again.";
            else if (UserMoney < Inventory[input].Price)
                return "Not enough money! Please insert more money.";
            else if (Inventory[input].Quantity == 0)
                return "That item is gone! Please select a different item.";
            else
            {
                double startLog = UserMoney;

                Inventory[input].ConsumeItem();
                string changedItem = Inventory[input].Name;
                if (SalesCount.ContainsKey(changedItem))
                    SalesCount[changedItem]++;

                UserMoney -= Inventory[input].Price;
                Bank += Inventory[input].Price;

                double endLog = UserMoney;
                ReadWriteData.WriteLog($"{Inventory[input].Name} {Inventory[input].Slot}", startLog, endLog);

                return Inventory[input].ConsumeMessage;
            }
        }
        public string MakeChange()
        {
            double startLog = UserMoney;
            UserMoney *= 100;

            if(UserMoney == 0)
            {
                return "";
            }

            int cents = Convert.ToInt32(UserMoney);
            int D = 0;
            int P = 0;
            int N = 0;
            string coins = "";

            int Q = cents / 25;
            if (Q > 0)
            {
                if (Q == 1)
                {
                    coins += Q + " Quarter ";
                }
                else
                {
                    coins += Q + " Quarters ";
                }
                cents %= 25;
            }
            D = cents / 10;
            if (D > 0)
            {
                if (D == 1)
                {
                    coins += D + " Dime ";
                }
                else
                {
                    coins += D + " Dimes ";
                }
                cents %= 10;
            }
            N = cents / 5;
            if (N > 0)
            {
                if (N == 1)
                {
                    coins += N + " Nickel ";
                }
                else
                {
                    coins += N + " Nickels ";
                }
                cents %= 5;
            }
            P = cents;
            if (P > 0)
            {
                if (P == 1)
                {
                    coins += P + " Penny ";
                }
                else
                {
                    coins += P + " Pennies ";
                }
            }

            UserMoney = 0;
            SalesCount["Bank"] = Convert.ToInt32(Bank*100);
            ReadWriteData.WriteSales(SalesCount);
            double endLog = UserMoney;
            ReadWriteData.WriteLog("MAKE CHANGE:", startLog, endLog);
            return coins + "returned.";
        }
    }
}
