using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Data
{
    class CarburetorPartStock : CarPartStock
    {
        public CarburetorPartStock()
        {
            for (int i = 0; i < 5; i++)
            {
                Stock.Add(new CarburetorPart(true));
            }
        }
    }
}
