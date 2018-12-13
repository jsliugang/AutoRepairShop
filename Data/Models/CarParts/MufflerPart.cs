namespace AutoRepairShop.Data.Models.CarParts
{
    internal class MufflerPart:CarPart
    {
        public MufflerPart(byte durability) : base("Muffler", durability)
        {
            Cost = 100;
        }
    }
}
