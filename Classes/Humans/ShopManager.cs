using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes;

namespace AutoRepairShop.Classes.Humans
{
    class ShopManager : Human
    {
        public ShopManager()
        {
            Name = "Lucy";
            Greet();
            Say("I am the Repair Shop manager! How can I help you?");
        }
    }
}
