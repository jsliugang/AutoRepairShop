using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    class WagonBuilder:CarBuilder
    {
        public WagonBuilder(bool random):base(random)
        {
            Car = new Wagon();
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
            CarNames.Add("Lada Granta");
            CarNames.Add("Audi RS4 V Avant");
        }
    }
}
