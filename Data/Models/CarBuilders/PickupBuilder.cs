using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class PickupBuilder:CarBuilder
    {
        public PickupBuilder()
        {
            Car = new Pickup();
            RandomCarName();
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
