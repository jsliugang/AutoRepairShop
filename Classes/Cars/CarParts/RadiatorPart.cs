using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class RadiatorPart:CarPart
    {
        public RadiatorPart(bool state) : base("Radiator", state)
        {
            Cost = 3500;
        }
    }
}
