using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    class Snowplug:CleaningVehicle
    {
        public void CleanSnow()
        {
            Console.WriteLine($"{Name} is cleaning the snow!");
        }
    }
}
