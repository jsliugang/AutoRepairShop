using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class CarburetorPartStock : CarPartStock<CarburetorPart>
    {
        public CarburetorPartStock()
        {
            Add(3);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new CarburetorPart(true));
            }
        }
    }
}
