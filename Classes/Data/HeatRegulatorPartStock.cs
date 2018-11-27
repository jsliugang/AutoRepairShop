using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Classes.Data
{
    class HeatRegulatorPartStock: CarPartStock<HeatRegulatorPart>
    {
        public HeatRegulatorPartStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new HeatRegulatorPart(true));
            }
        }
    }
}
