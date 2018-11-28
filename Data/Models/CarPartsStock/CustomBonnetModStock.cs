using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class CustomBonnetModStock : CarPartStock<CustomBonnetMod>
    {
        public CustomBonnetModStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new CustomBonnetMod(true));
            }
        }
    }
}
