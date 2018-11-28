using System;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Base
{
    abstract class CleaningVehicle:SpecialCar, IRadio
    {
        protected CleaningVehicle()
        {
        }

        public bool IsWorking { get; set; }
        public bool RadioState { get; set; }

        public override void Honk()
        {
            Console.WriteLine($"Sir, the {Name} cannot honk. Please resume you duties!");
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
