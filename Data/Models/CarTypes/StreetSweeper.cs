using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    class StreetSweeper:CleaningVehicle
    {
        public void CleanStreet()
        {
            Console.WriteLine($"{Name} is cleaning the street!");
        }
    }
}
