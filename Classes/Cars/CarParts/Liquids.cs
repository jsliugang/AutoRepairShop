using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class Liquids
    {
        byte? Fuel { get; set; }
        byte? EngineOil { get; set; }
        byte? BrakeFluid { get; set; }
        byte? CoolingLiquid { get; set; }
        byte? WindshieldWasherLiquid { get; set; }

        public Liquids()
        {
            Fuel = 50;
            EngineOil = 50;
            BrakeFluid = 50;
            CoolingLiquid = 50;
            WindshieldWasherLiquid = 50;
        }
    }
}
