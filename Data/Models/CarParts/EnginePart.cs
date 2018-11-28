﻿using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class EnginePart:CarPart
    {
        public EnginePart(bool state) : base("Engine", state)
        {
            Cost = 3000;
        }

        public bool CheckFuel(Liquids carLiquids)
        {
            byte fuelLevel;
            carLiquids.CarLiquids.TryGetValue("Fuel", out fuelLevel);

            return fuelLevel >= 1;
        }
    }
}
