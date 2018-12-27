namespace AutoRepairShop.Data.Models.CarParts
{
    internal class DecalsMod:CarPart
    {
        public DecalsMod(byte durability) : base("Decals", durability)
        {
            Cost = 100;
        }
    }
}
