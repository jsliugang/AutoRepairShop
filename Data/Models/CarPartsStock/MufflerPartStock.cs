using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class MufflerPartStock: CarPartStock<MufflerPart>
    {
        public MufflerPartStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new MufflerPart(true));
            }
        }
    }
}
