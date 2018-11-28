using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class TitaniumWipersMod:CarPart
    {
        public TitaniumWipersMod(bool state) : base("TitaniumWipers", state)
        {
            Cost = 85;
        }
    }
}
