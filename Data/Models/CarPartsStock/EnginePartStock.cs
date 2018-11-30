using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class EnginePartStock : CarPartStock<EnginePart>
    {
        public EnginePartStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new EnginePart(true));
            }
        }
    }
}
