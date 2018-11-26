using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class TitaniumWipersMod:CarPart
    {
        public TitaniumWipersMod(bool state) : base("TitaniumWipers", state)
        {
            Cost = 85;
        }
    }
}
