using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class DumpTruckBuilder:CarBuilder
    {
        public DumpTruckBuilder()
        {
            Car = new DumpTruck();
            ProcessNamingInput();
        }

        public override void CreateCar()
        {
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("BELAZ 75710");
            CarNames.Add("Caterpillar 797F");
            CarNames.Add("Liebherr T 282");
            CarNames.Add("Komatsu 980E");
            CarNames.Add("Terex 33-19 Titan");
        }
    }
}
