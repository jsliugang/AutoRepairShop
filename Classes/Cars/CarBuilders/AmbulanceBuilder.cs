using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class AmbulanceBuilder:CarBuilder
    {

        public AmbulanceBuilder(bool random):base(random)
        {
            Car = new Ambulance();
            if (random)
            {
                RandomCarName();
            }
            else
            {
                ProcessNamingInput();
            }
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("Emergency 911 Ambulance");
            CarNames.Add("Regular Ambulance");
        }
    }
}
