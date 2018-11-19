using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Humans
{
    class Customer:Human
    {
        public Car MyCar { get; set; }

        public Customer()
        {
            Menu.PrintMenuMessage("Please set new customer's name:");
            Name = Console.ReadLine();
            Menu.PrintMenuMessage($"New Customer has arrived! Name - {Name}");
            MyCar = new PassengerCar();

        }

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            base.Say(message);
            Console.ResetColor();
        }

        public void MakeDiagnosticsOrder()
        {
            Say($"Please diagnoze my {MyCar.Name}, I need to know what is broken");
            ShopManager.TakeCar(MyCar);
        }

        public void MakeRepairOrder()
        {
            Say($"Please repair all the broken parts of my {MyCar.Name}.");
            ShopManager.TakeCar(MyCar);
        }

        public void PimpMyCar()
        {
            Say($"Xzibit, pimp my {MyCar.Name}!!");
            ShopManager.TakeCar(MyCar);
        }

        public void RepairBrokenParts()
        {
            Say($"Replace all broken parts in {MyCar.Name}, please...");
            ShopManager.TakeCar(MyCar);
        }

    }
}
