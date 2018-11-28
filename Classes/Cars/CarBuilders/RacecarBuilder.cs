using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class RacecarBuilder:CarBuilder
    {
        public RacecarBuilder(bool random):base(random)
        {
            Car = new Racecar();
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
            CarNames.Add("Buggy");
            CarNames.Add("Panoz GF09");
            CarNames.Add("Caterham CT03");
            CarNames.Add("FIA GT1");
            CarNames.Add("Audi R18");
        }
    }
}
