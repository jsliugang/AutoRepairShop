using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class OffroaderBuilder:CarBuilder
    {
        public OffroaderBuilder()
        {
            Car = new Offroader();
            RandomCarName();
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("UAZ Patriot");
            CarNames.Add("Jeep Wrangler");
            CarNames.Add("Suzuki Jimny");
            CarNames.Add("Jeep Grand Cherokee");
            CarNames.Add("Range Rover Velar");
            CarNames.Add("Chevrolet Tahoe");
        }
    }
}
