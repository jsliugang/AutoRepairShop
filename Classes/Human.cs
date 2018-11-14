using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes
{
    abstract class Human
    {
        public string Name => "Human";

        public void Greet()
        {
            Console.WriteLine($"Hello! My name is {Name}");
        }

        public void Say(string message)
        {
            
        }



    }
}
