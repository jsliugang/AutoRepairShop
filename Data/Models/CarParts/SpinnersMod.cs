namespace AutoRepairShop.Data.Models.CarParts
{
    class SpinnersMod:CarPart
    {
        public SpinnersMod(byte durability) : base("Spinners", durability)
        {
            Cost = 500;
        }
    }
}
