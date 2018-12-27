namespace AutoRepairShop.Data.Models.CarParts
{
    internal class SportSuspensionMod:CarPart
    {
        public SportSuspensionMod(byte durability) : base("SportSuspension", durability)
        {
            Cost = 1230;
        }
    }
}
