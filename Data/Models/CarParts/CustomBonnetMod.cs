namespace AutoRepairShop.Data.Models.CarParts
{
    internal class CustomBonnetMod:CarPart
    {
        public CustomBonnetMod(byte durability) :base("CustomBonnet", durability)
        {
            Cost = 300;
        }
    }
}
