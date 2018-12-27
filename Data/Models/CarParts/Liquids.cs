using System.Collections.Generic;

namespace AutoRepairShop.Data.Models.CarParts
{
    internal class Liquids
    {
        public Dictionary<string, int> CarLiquids = new Dictionary<string, int>();

        public Liquids()
        {
            CarLiquids.Add("Fuel", 50);
            CarLiquids.Add("EngineOil", 50);
            CarLiquids.Add("BrakeFluid", 50);
            CarLiquids.Add("CoolingLiquid", 50);
            CarLiquids.Add("WindshieldWasherLiquid", 50);
        }

        public void UpdateAmount(string key, int amount)
        {
            CarLiquids[key] = amount;
        }
    }
}
