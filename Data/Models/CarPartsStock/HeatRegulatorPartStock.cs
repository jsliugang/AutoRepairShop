using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
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
