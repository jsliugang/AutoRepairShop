namespace AutoRepairShop.Data.Models.CarParts
{
    internal class SpinnersMod:CarPart
    {
        public SpinnersMod(byte durability) : base("Spinners", durability)
        {
            Cost = 500;
        }
    }
}
