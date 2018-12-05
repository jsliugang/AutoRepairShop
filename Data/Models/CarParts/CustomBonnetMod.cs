namespace AutoRepairShop.Data.Models.CarParts
{
    class CustomBonnetMod:CarPart
    {
        public CustomBonnetMod(byte durability) :base("CustomBonnet", durability)
        {
            Cost = 300;
        }
    }
}
