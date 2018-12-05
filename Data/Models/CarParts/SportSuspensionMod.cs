namespace AutoRepairShop.Data.Models.CarParts
{
    class SportSuspensionMod:CarPart
    {
        public SportSuspensionMod(byte durability) : base("SportSuspension", durability)
        {
            Cost = 1230;
        }
    }
}
