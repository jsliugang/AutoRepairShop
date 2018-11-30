using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    abstract class CleaningVehicle:SpecialCar, IRadio
    {
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
