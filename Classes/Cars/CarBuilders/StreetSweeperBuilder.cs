using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class StreetSweeperBuilder:CarBuilder
    {
        public StreetSweeperBuilder()
        {
            Car = new StreetSweeper();
            ProcessNamingInput();
        }

        public override void CreateCar()
        {
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("MX - 450");
            CarNames.Add("Dulevo sweeper");
        }
    }
}
