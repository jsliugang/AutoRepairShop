using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class SpoilerModStock: CarPartStock<SpoilerMod>
    {
        public SpoilerModStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new SpoilerMod(SetRandomDurability()));
            }
        }
    }
}
