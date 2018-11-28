using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class SpinnersMod:CarPart
    {
        public SpinnersMod(bool state) : base("Spinners", state)
        {
            Cost = 500;
        }
    }
}
