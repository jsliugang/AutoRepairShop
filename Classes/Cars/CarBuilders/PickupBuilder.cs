using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class PickupBuilder:CarBuilder
    {
        public PickupBuilder(bool random):base(random)
        {
            Car = new Pickup();
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
            CarNames.Add("Toyota Tundra");
            CarNames.Add("VW Amarok");
            CarNames.Add("Toyota Tacoma");
            CarNames.Add("Dodge Ram");
            CarNames.Add("Ford F250");
            CarNames.Add("Nissan Navara");
        }
    }
}
