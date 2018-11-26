using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class HeatRegulatorPart:CarPart
    {
        public HeatRegulatorPart(bool state) : base("HeatRegulator", state)
        {
            Cost = 200;
        }
    }
}
