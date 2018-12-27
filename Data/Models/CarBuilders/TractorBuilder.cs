using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class TractorBuilder:CarBuilder
    {
        public TractorBuilder()
        {
            Car = new Tractor();
            RandomCarName();
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("Ursus 11054");
            CarNames.Add("Fendt 820");
            CarNames.Add("MTZ 122");
        }
    }
}
