using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    internal class Snowplug:CleaningVehicle
    {
        public void CleanSnow()
        {
            Console.WriteLine($"{Name} is cleaning the snow!");
        }
    }
}
