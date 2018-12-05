namespace AutoRepairShop.Data.Models.CarParts
{
    class DecalsMod:CarPart
    {
        public DecalsMod(byte durability) : base("Decals", durability)
        {
            Cost = 100;
        }
    }
}
