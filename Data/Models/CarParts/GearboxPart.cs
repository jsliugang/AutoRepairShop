using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class GearboxPart:CarPart
    {
        public GearboxPart(bool state) : base("Gearbox", state)
        {
            Cost = 2500;
        }
    }
}
