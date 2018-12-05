﻿using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class SpinnersModStock: CarPartStock<SpinnersMod>
    {
        public SpinnersModStock()
        {
            Add(5);
        }

        public override void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Stock.Add(new SpinnersMod(SetRandomDurability()));
            }
        }
    }
}
