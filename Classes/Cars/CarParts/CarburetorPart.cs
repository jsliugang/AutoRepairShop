using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class CarburetorPart:CarPart
    {
        public CarburetorPart(bool state) : base("Carburetor", state)
        {
            Cost = 100;
        }
    }
}
