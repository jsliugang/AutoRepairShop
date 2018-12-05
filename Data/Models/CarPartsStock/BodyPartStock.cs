using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class BodyPartStock: CarPartStock<BodyPart>
    {
        public BodyPartStock()
        {
            Add(3);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new BodyPart(SetRandomDurability()));
            }
        }
    }
}
