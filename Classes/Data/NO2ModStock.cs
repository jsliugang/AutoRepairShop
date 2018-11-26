using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Classes.Data
{
    class NO2ModStock: CarPartStock
    {
        public NO2ModStock()
        {
            for (int i = 0; i < 5; i++)
            {
                Stock.Add(new NO2Mod(true));
            }
        }

    }
}
