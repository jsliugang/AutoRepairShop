using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    class TractorBuilder:CarBuilder
    {
        public TractorBuilder(bool random):base(random)
        {
            Car = new Tractor();
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
            CarNames.Add("Ursus 11054");
            CarNames.Add("Fendt 820");
            CarNames.Add("MTZ 122");
        }
    }
}
