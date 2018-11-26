using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class CarHaulerBuilder:CarBuilder
    {
        public CarHaulerBuilder()
        {
            Car = new CarHauler();
            ProcessNamingInput();
        }

        public override void CreateCar()
        {
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("Lohr");
            CarNames.Add("Rolfo");
            CarNames.Add("Kassbohrer");
        }
    }
}
