using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    class OffroaderBuilder:CarBuilder
    {
        public OffroaderBuilder(bool random):base(random)
        {
            Car = new Offroader();
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
            CarNames.Add("UAZ Patriot");
            CarNames.Add("Jeep Wrangler");
            CarNames.Add("Suzuki Jimny");
            CarNames.Add("Jeep Grand Cherokee");
            CarNames.Add("Range Rover Velar");
            CarNames.Add("Chevrolet Tahoe");
        }
    }
}
