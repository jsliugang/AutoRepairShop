using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class RacecarBuilder:CarBuilder
    {
        public RacecarBuilder()
        {
            Car = new Racecar();
            RandomCarName();
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
