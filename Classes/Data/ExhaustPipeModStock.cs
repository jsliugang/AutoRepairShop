﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Classes.Data
{
    class ExhaustPipeModStock : CarPartStock
    {
        public ExhaustPipeModStock()
        {
            for (int i = 0; i < 5; i++)
            {
                Stock.Add(new ExhaustPipeMod(true));
            }
        }
    }
}
