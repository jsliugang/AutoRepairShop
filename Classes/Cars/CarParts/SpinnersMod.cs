using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class SpinnersMod:CarPart
    {
        public SpinnersMod(bool state) : base("Spinners", state)
        {
            Cost = 500;
        }
    }
}
