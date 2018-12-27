namespace AutoRepairShop.Data.Models.CarParts
{
    internal class WheelsPart:CarPart
    {
        public WheelsPart(byte durability) : base("Wheels", durability)
        {
            Cost = 500;
        }
    }
}
