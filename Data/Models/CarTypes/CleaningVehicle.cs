using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    internal abstract class CleaningVehicle:SpecialCar, IRadio
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
            Console.WriteLine(RadioState ? $"Radio switched on!" : $"Radio switched off!");
        }
    }
}
