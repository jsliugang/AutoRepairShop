namespace AutoRepairShop.Data.Models.CarParts
{
    internal class TitaniumWipersMod:CarPart
    {
        public TitaniumWipersMod(byte durability) : base("TitaniumWipers", durability)
        {
            Cost = 85;
        }
    }
}
