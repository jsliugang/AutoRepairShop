namespace AutoRepairShop.Data.Models.CarParts
{
    class HornPart:CarPart
    {
        public HornPart(byte durability) : base("Horn", durability)
        {
            Cost = 50;
        }

        
    }
}
