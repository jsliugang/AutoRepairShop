namespace AutoRepairShop.Data.Models.CarParts
{
    internal class SpoilerMod:CarPart
    {
        public SpoilerMod(byte durability) : base("Spoiler", durability)
        {
            Cost = 230;
        }
    }
}
