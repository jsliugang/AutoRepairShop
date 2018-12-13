namespace AutoRepairShop.Data.Models.CarParts
{
    internal class HeatRegulatorPart:CarPart
    {
        public HeatRegulatorPart(byte durability) : base("HeatRegulator", durability)
        {
            Cost = 200;
        }
    }
}
