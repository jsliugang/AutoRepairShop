using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class SportSuspensionMod:CarPart
    {
        public SportSuspensionMod(bool state) : base("SportSuspension", state)
        {
            Cost = 1230;
        }
    }
}
