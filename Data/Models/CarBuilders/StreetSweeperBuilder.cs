using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class StreetSweeperBuilder:CarBuilder
    {
        public StreetSweeperBuilder()
        {
            Car = new StreetSweeper();
            RandomCarName();
        }

        public override void CreateCar()
        {
            SetBody();
            SetCarburetor();
            SetEngine();
            SetGearbox();
            SetHeatRegulator();
            SetLiquids();
            SetMuffler();
            SetRadiator();
            SetWheels();
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("MX - 450");
            CarNames.Add("Dulevo sweeper");
        }
    }
}
