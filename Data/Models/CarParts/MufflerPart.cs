namespace AutoRepairShop.Data.Models.CarParts
{
    class MufflerPart:CarPart
    {
        public MufflerPart(byte durability) : base("Muffler", durability)
        {
            Cost = 100;
        }
    }
}
