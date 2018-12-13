using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class DumpTruckBuilder:CarBuilder
    {
        public DumpTruckBuilder()
        {
            Car = new DumpTruck();
            RandomCarName();          
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("BELAZ 75710");
            CarNames.Add("Caterpillar 797F");
            CarNames.Add("Liebherr T 282");
            CarNames.Add("Komatsu 980E");
            CarNames.Add("Terex 33-19 Titan");
        }
    }
}
