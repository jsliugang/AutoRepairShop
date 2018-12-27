using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    internal class EnginePartStock : CarPartStock<EnginePart>
    {
        public EnginePartStock()
        {
            AddMany(5);
        }

        public override void AddMany(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                Stock.Add(new EnginePart(SetRandomDurability()));
            }
        }
    }
}
