using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Item
    {
        public int Quantity { get; set; }
        public string Slot { get; }
        public string Name { get; }
        public string ConsumeMessage { get; }
        public double Price { get; }

        public Item(string slot, string name, double price)
        {
            Slot = slot;
            Name = name;
            Price = price;
            Quantity = 5;

            if (slot[0].ToString() == "A")
                ConsumeMessage = "Crunch Crunch, Yum!";
            else if (slot[0].ToString() == "B")
                ConsumeMessage = "Munch Munch, Yum!";
            else if (slot[0].ToString() == "C")
                ConsumeMessage = "Glug Glug, Yum!";
            else if (slot[0].ToString() == "D")
                ConsumeMessage = "Chew Chew, Yum!";
        }

        public void ConsumeItem()
        {
            Quantity--;
        }
    }
}
