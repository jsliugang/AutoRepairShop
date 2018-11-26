﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class MufflerPart:CarPart
    {
        public MufflerPart(bool state) : base("Muffler", state)
        {
            Cost = 100;
        }
    }
}
