namespace AutoRepairShop.Data.Models.CarParts
{
    class GearboxPart:CarPart
    {
        public GearboxPart(byte durability) : base("Gearbox", durability)
        {
            Cost = 2500;
        }
    }
}
