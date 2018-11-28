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
        public CarHaulerBuilder(bool random):base(random)
        {
            Car = new CarHauler();
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
            CarNames.Add("Lohr");
            CarNames.Add("Rolfo");
            CarNames.Add("Kassbohrer");
        }
    }
}
