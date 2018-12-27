namespace AutoRepairShop.Data.Models.CarParts
{
    internal class ExhaustPipeMod: CarPart
    {
        public ExhaustPipeMod(byte durability) : base("ExhaustPipe", durability)
        {
            Cost = 120;
        }
    }
}
