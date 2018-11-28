using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
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
