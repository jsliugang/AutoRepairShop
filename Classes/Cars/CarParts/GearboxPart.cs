using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class GearboxPart:CarPart
    {
        public GearboxPart(bool state) : base("Gearbox", state)
        {
            Cost = 2500;
        }
    }
}
