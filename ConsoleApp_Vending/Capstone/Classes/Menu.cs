using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Menu
    {
        private VendingMachine myVendor;

        public Menu(VendingMachine myVendor)
        {
            this.myVendor = myVendor;
        }

        public void Running()
        {
            bool running = true;

            while (running)
            {
                running = MainMenu();
            }
            Environment.Exit(0);
        }
        public bool MainMenu()
        {
            Console.Clear();

            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) End Program");
            Console.WriteLine();

            char userInput = Console.ReadKey(true).KeyChar;

            if (userInput == '1')
            {
                Console.Clear();

                foreach (string s in myVendor.PrintInventory())
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                Console.Write("Press any key to return to Main Menu...");
                Console.ReadKey();

                return true;
            }
            else if (userInput == '2')
            {
                bool running = true;
                while (running)
                {
                    running = PurchaseMenu();
                }

                return true;
            }
            else if (userInput == '3')
            {
                return false;
            }
            else if (userInput == '4')
            {
                Console.Clear();

                Console.WriteLine("Sales Report");
                Console.WriteLine("------------------------------------");
                foreach (KeyValuePair<string, int> n in ReadWriteData.ReadSales())
                {
                    if (n.Key == "Bank")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Total Sales: " + ToDollars((Convert.ToDouble(n.Value))/100));
                        Console.WriteLine();
                    }
                    else
                        Console.WriteLine(n.Key.PadRight(20) + n.Value);
                    
                }

                Console.WriteLine();
                Console.Write("Press any key to return to Main Menu...");
                Console.ReadKey();

                return true;
            }
            else
            {
                Console.Write("Please choose a valid menu option. Press any key to continue...");
                Console.ReadKey();

                return true;
            }
        }
        public bool PurchaseMenu()
        {
            Console.Clear();

            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine($"Current Money Provided: {ToDollars(myVendor.UserMoney)}");
            Console.WriteLine();

            char userInput = Console.ReadKey(true).KeyChar;

            if (userInput == '1')
            {
                Console.Clear();
                Console.Write("How much money would you like to insert? (in whole dollars, change is not accepted) ");

                bool isNumber = Int32.TryParse(Console.ReadLine(), out int feedMoney);

                if (isNumber && feedMoney > 0)
                {
                    myVendor.FeedMoney(feedMoney);
                    return true;
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Please insert a valid amount of money! Press any key to continue...");
                    Console.ReadKey();
                    return true;
                }
            }
            else if (userInput == '2')
            {
                Console.Write("Please enter a product to purchase (i.e. A4): ");
                string purchasedProduct = Console.ReadLine();
                Console.Write(myVendor.SelectProduct(purchasedProduct) + " Press any key to continue...");
                Console.ReadKey();
                return true;
            }
            else if (userInput == '3')
            {
                Console.WriteLine(myVendor.MakeChange());
                Console.Write("Press any key to return to the main menu...");
                Console.ReadKey();
                return false;
            }
            else
            {
                Console.Write("Please choose a valid menu option. Press any key to continue...");
                Console.ReadLine();
                return true;
            }

        }
        public static string ToDollars(double cost)
        {
            string costString;
            int centAmt = Convert.ToInt32(cost * 100);
            int cent = centAmt % 100;
            int dollar = centAmt / 100;
            if (cent < 10)
            {
                costString = "$" + dollar + ".0" + cent;
            }
            else
            {
                costString = "$" + dollar + "." + cent;
            }
            return costString;
        }
    }
}
