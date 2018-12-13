using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class PrimeMoverBuilder:CarBuilder
    {
        public PrimeMoverBuilder()
        {
            Car = new PrimeMover();
            RandomCarName();
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("MAN TGS 6x6");
            CarNames.Add("Foton AumanH5 4x2");
            CarNames.Add("DAF LF 55");
            CarNames.Add("Volvo FL 7");
            CarNames.Add("DAF XF 95 FAR");
            CarNames.Add("MAN TGX");
        }
    }
}
