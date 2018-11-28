using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class No2Mod:CarPart
    {
        public No2Mod(bool state) : base("NO2", state)
        {
            Cost = 1000;
        }
    }
}
