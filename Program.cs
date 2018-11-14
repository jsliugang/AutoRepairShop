using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes;
using AutoRepairShop.Classes.Humans;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            ShopManager sm = new ShopManager();


            Human myCustomer = new Customer();
            Console.ReadLine();
        }
    }
}
