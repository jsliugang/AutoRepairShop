using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class OffroaderBuilder:CarBuilder
    {
        public OffroaderBuilder(bool random):base(random)
        {
            Car = new Offroader();
            if (random)
            {
                RandomCarName();
            }
            else
            {
                ProcessNamingInput();
            }
        }

        public override void CreateCar()
        {
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("UAZ Patriot");
            CarNames.Add("Jeep Wrangler");
            CarNames.Add("Suzuki Jimny");
            CarNames.Add("Jeep Grand Cherokee");
            CarNames.Add("Range Rover Velar");
            CarNames.Add("Chevrolet Tahoe");
        }
    }
}
