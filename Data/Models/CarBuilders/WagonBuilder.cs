using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class WagonBuilder:CarBuilder
    {
        public WagonBuilder()
        {
            Car = new Wagon();
            RandomCarName();
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("Lada Granta");
            CarNames.Add("Audi RS4 V Avant");
        }
    }
}
