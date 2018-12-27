namespace AutoRepairShop.Data.Models.CarParts
{
    internal class HornPart:CarPart
    {
        public HornPart(byte durability) : base("Horn", durability)
        {
            Cost = 50;
        }       
    }
}
