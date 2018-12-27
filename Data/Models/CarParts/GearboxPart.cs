namespace AutoRepairShop.Data.Models.CarParts
{
    internal class GearboxPart:CarPart
    {
        public GearboxPart(byte durability) : base("Gearbox", durability)
        {
            Cost = 2500;
        }
    }
}
