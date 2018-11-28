using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class HornPart:CarPart
    {
        public HornPart(bool state) : base("Horn", state)
        {
            Cost = 50;
        }

        
    }
}
