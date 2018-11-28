using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.CarParts
{
    class CustomBonnetMod:CarPart
    {
        public CustomBonnetMod(bool state):base("CustomBonnet", state)
        {
            Cost = 300;
        }
    }
}
