using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Cars.CarTypes
{
    class CleaningVehicle:SpecialCar, IRadio
    {
        protected CleaningVehicle()
        {
            RemoveHorn();
        }

        public bool IsWorking { get; set; }
        public bool RadioState { get; set; }

        public override void Honk()
        {
            Console.WriteLine($"Sir, the {Name} cannot honk. Please resume you duties!");
        }

        public void RemoveHorn()
        {
            horn = null;
        }

        public void SwitchRadio()
        {
            RadioState = !RadioState;
            if (RadioState)
            {
                Console.WriteLine($"Radio switched on!");
            }
            else
            {
                Console.WriteLine($"Radio switched off!");
            }
        }
    }
}
