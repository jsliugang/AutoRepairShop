using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Data
{
    class BodyPartStock: CarPartStock
    {
        public BodyPartStock()
        {
            for (int i = 0; i < 5; i++)
            {
                Stock.Add(new BodyPart(true));
            }
        }
    }
}
