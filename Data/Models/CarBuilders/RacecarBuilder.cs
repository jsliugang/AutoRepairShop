using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    class RacecarBuilder:CarBuilder
    {
        public RacecarBuilder(bool random):base(random)
        {
            Car = new Racecar();
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
            CarNames.Add("Buggy");
            CarNames.Add("Panoz GF09");
            CarNames.Add("Caterham CT03");
            CarNames.Add("FIA GT1");
            CarNames.Add("Audi R18");
        }
    }
}
