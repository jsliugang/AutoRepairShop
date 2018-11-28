using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class RadiatorPart:CarPart
    {
        public RadiatorPart(bool state) : base("Radiator", state)
        {
            Cost = 3500;
        }
    }
}
