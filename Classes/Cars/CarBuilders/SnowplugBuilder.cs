using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class SnowplugBuilder:CarBuilder
    {
        public SnowplugBuilder(bool random):base(random)
        {
            Car = new Snowplug();
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
            CarNames.Add("Snowplug ZLST551Q");
            CarNames.Add("Petrol 11 HP snowplug STG1101QE-02");
        }
    }
}
