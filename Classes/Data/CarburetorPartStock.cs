using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Data
{
    class CarburetorPartStock : CarPartStock<CarburetorPart>
    {
        public CarburetorPartStock()
        {
            Add(3);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new CarburetorPart(true));
            }
        }
    }
}
