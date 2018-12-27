using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class AmbulanceBuilder:CarBuilder
    {
        public AmbulanceBuilder()
        {
            Car = new Ambulance();
            RandomCarName();
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("Emergency 911 Ambulance");
            CarNames.Add("Regular Ambulance");
        }
    }
}
