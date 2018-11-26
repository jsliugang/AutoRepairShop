using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class WheelsPart:CarPart
    {
        public WheelsPart(bool state) : base("Wheels", state)
        {
            Cost = 500;
        }
    }
}
