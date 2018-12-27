using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    internal class StreetSweeper:CleaningVehicle
    {
        public void CleanStreet()
        {
            Console.WriteLine($"{Name} is cleaning the street!");
        }
    }
}
