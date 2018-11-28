using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class TitaniumWipersModStock:CarPartStock<TitaniumWipersMod>
    {
        public TitaniumWipersModStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new TitaniumWipersMod(true));
            }
        }
    }
}
