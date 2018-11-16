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

        }

        public bool CheckFuel(Liquids carLiquids)
        {
            if (carLiquids.Fuel < 1)
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
