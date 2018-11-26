using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class HornPart:CarPart
    {
        public HornPart(bool state) : base("Horn", state)
        {
            Cost = 50;
        }

        
    }
}
