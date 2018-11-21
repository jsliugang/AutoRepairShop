using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class EnginePart:CarPart
    {
        public EnginePart() : base("Engine")
        {
            Cost = 3000;
        }

        public bool CheckFuel(Liquids carLiquids)
        {
            byte fuelLevel;
            carLiquids.CarLiquids.TryGetValue("Fuel", out fuelLevel);

            if (fuelLevel < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
