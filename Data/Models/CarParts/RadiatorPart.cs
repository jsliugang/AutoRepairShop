namespace AutoRepairShop.Data.Models.CarParts
{
    internal class RadiatorPart:CarPart
    {
        public RadiatorPart(byte durability) : base("Radiator", durability)
        {
            Cost = 350;
        }
    }
}
