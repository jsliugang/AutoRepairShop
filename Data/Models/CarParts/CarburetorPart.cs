using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class CarburetorPart:CarPart
    {
        public CarburetorPart(bool state) : base("Carburetor", state)
        {
            Cost = 100;
        }
    }
}
