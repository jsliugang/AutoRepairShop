using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarTypes
{
    class StreetSweeper:CleaningVehicle
    {
        public void CleanStreet()
        {
            Console.WriteLine($"{Name} is cleaning the street!");
        }
    }
}
