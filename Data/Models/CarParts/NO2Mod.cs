namespace AutoRepairShop.Data.Models.CarParts
{
    class No2Mod:CarPart
    {
        public No2Mod(byte durability) : base("NO2", durability)
        {
            Cost = 1000;
        }
    }
}
