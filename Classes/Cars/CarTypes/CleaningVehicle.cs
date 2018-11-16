using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Classes.Cars.CarTypes
{
    class CleaningVehicle:SpecialCar
    {
        protected CleaningVehicle()
        {
            RemoveHorn();
        }

        public override void Honk()
        {
            Console.WriteLine($"Sir, the {Name} cannot honk. Please resume you duties!");
        }

        public void RemoveHorn()
        {
            horn = null;
        }
    }
}
