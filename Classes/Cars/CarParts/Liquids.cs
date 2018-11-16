using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class Liquids
    {
        public byte Fuel { get; set; }
        public byte EngineOil { get; set; }
        public byte BrakeFluid { get; set; }
        public byte CoolingLiquid { get; set; }
        public byte WindshieldWasherLiquid { get; set; }

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
