using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    internal class SnowplugBuilder:CarBuilder
    {
        public SnowplugBuilder()
        {
            Car = new Snowplug();
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
            CarNames.Add("Snowplug ZLST551Q");
            CarNames.Add("Petrol 11 HP snowplug STG1101QE-02");
        }
    }
}
