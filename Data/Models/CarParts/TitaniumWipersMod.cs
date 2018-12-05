namespace AutoRepairShop.Data.Models.CarParts
{
    class TitaniumWipersMod:CarPart
    {
        public TitaniumWipersMod(byte durability) : base("TitaniumWipers", durability)
        {
            Cost = 85;
        }
    }
}
