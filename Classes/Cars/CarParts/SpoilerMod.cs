using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class SpoilerMod:CarPart
    {
        public SpoilerMod(bool state) : base("Spoiler", state)
        {
            Cost = 230;
        }
    }
}
