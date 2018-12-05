namespace AutoRepairShop.Data.Models.CarParts
{
    class ExhaustPipeMod: CarPart
    {
        public ExhaustPipeMod(byte durability) : base("ExhaustPipe", durability)
        {
            Cost = 120;
        }
    }
}
