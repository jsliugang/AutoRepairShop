using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    internal class BodyPartStock: CarPartStock<BodyPart>
    {
        public BodyPartStock()
        {
            AddMany(3);
        }

        public override void AddMany(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                Stock.Add(new BodyPart(SetRandomDurability()));
            }
        }
    }
}
