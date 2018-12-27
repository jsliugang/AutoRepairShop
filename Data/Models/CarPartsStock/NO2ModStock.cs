using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    internal class No2ModStock: CarPartStock<No2Mod>
    {
        public No2ModStock()
        {
            AddMany(5);
        }

        public override void AddMany(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                Stock.Add(new No2Mod(SetRandomDurability()));
            }
        }
    }
}
