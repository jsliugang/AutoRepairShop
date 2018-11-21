using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class Liquids
    {
        public Dictionary<string, byte> CarLiquids = new Dictionary<string, byte>(); 

        public Liquids()
        {
            CarLiquids.Add("Fuel", 50);
            CarLiquids.Add("EngineOil", 50);
            CarLiquids.Add("BrakeFluid", 50);
            CarLiquids.Add("CoolingLiquid", 50);
            CarLiquids.Add("WindshieldWasherLiquid", 50);
        }
    }
}
