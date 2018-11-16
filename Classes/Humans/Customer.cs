using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Humans
{
    class Customer:Human
    {
        public Car MyCar { get; set; }

        public Customer()
        {
            Console.WriteLine("Please set new customer's name:");
            Name = Console.ReadLine();
            Console.WriteLine($"New Customer has arrived! Name - {Name}");
            MyCar = new PassengerCar();

        }

    }
}
