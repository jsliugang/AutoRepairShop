using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class CustomBonnetMod:CarPart
    {
        public CustomBonnetMod(bool state):base("CustomBonnet", state)
        {
            Cost = 300;
        }
    }
}
