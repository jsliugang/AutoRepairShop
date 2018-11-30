using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class RadiatorPartStock: CarPartStock<RadiatorPart>
    {
        public RadiatorPartStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new RadiatorPart(true));
            }
        }
    }
}
