namespace AutoRepairShop.Data.Models.CarParts
{
    class HeatRegulatorPart:CarPart
    {
        public HeatRegulatorPart(bool state) : base("HeatRegulator", state)
        {
            Cost = 200;
        }
    }
}
