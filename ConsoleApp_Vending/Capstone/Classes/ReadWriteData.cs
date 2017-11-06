using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class ReadWriteData
    {
        public static Dictionary<string, Item> ReadInventory()
        {
            Dictionary<string, Item> inventory = new Dictionary<string, Item>();

            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                string logName = "vendingmachine.csv";

                using (StreamReader sr = new StreamReader(Path.Combine(currentDirectory, logName)))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] lineArray = sr.ReadLine().Split('|');

                        string slot = lineArray[0];
                        string name = lineArray[1];
                        double price = Double.Parse(lineArray[2]);

                        inventory.Add(slot, new Item(slot, name, price));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to restock Vendo-Matic 500 properly!");
                Console.WriteLine(e.Message);
            }

            return inventory;
        }

        public static void WriteLog(string transactionDescription, double startLog, double endLog)
        {
            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                string logName = "vendingmachinelog.txt";

                using (StreamWriter sw = new StreamWriter(Path.Combine(currentDirectory,logName), true))
                {
                    DateTime time = DateTime.UtcNow;
                    string logItem = "";
                    logItem += $"{time.ToString()} {transactionDescription.PadRight(20)} {Menu.ToDollars(startLog).PadRight(10)} {Menu.ToDollars(endLog)}";
                    sw.WriteLine(logItem);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to log action properly!");
                Console.WriteLine(e.Message);
            }
        }

        public static void WriteSales(Dictionary<string, int> salesCount)
        {
            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                string logName = "totalSales.txt";

                using (StreamWriter sw = new StreamWriter(Path.Combine(currentDirectory, logName), false))
                {
                    foreach (KeyValuePair<string, int> k in salesCount)
                    {
                        sw.WriteLine(k.Key + "|" + k.Value);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to log new sales!");
                Console.WriteLine(e.Message);
            }
        }

        public static Dictionary<string, int> ReadSales()
        {
            Dictionary<string, int> sales = new Dictionary<string, int>();
            try
            {
                string currentDirectory = Environment.CurrentDirectory;
                string logName = "totalSales.txt";

                using (StreamReader sr = new StreamReader(Path.Combine(currentDirectory, logName)))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] countArr = sr.ReadLine().Split('|');

                        string item = countArr[0];
                        int count = Convert.ToInt32(countArr[1]);

                        sales.Add(item, count);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to read Sales Count");
                Console.WriteLine(e.Message);
            }
            return sales;
        }
    }
}


