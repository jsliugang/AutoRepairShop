using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class CarHaulerBuilder:CarBuilder
    {
        public CarHaulerBuilder()
        {
            Car = new CarHauler();
            RandomCarName();
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("Lohr");
            CarNames.Add("Rolfo");
            CarNames.Add("Kassbohrer");
        }
    }
}
