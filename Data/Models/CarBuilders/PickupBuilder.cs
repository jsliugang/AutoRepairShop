using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
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
