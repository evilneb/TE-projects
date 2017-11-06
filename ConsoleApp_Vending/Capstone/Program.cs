using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine myVendor = new VendingMachine();
            Menu myMenu = new Menu(myVendor);
            myMenu.Running();
        }
    }
}
