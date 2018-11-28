using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class ExhaustPipeMod: CarPart
    {
        public ExhaustPipeMod(bool state) : base("ExhaustPipe", state)
        {
            Cost = 120;
        }
    }
}
