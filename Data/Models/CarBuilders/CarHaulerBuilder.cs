using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
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
