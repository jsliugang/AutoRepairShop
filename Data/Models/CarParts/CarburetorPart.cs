namespace AutoRepairShop.Data.Models.CarParts
{
    class CarburetorPart:CarPart
    {
        public CarburetorPart(byte durability) : base("Carburetor", durability)
        {
            Cost = 100;
        }
    }
}
