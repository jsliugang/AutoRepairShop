using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class ExhaustPipeMod: CarPart
    {
        public ExhaustPipeMod(bool state) : base("ExhaustPipe", state)
        {
            Cost = 120;
        }
    }
}
