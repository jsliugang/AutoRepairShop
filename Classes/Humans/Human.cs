﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AutoRepairShop.Classes.Humans
{
    abstract class Human
    {
        public string Name { get; set; }

        public void Greet()
        {
            Console.WriteLine($"Hello! My name is {Name}");
        }

        public void Say(string message)
        {
            Console.WriteLine(message);
        }



    }
}
