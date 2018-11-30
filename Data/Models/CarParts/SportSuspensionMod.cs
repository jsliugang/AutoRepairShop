namespace AutoRepairShop.Data.Models.CarParts
{
    class SportSuspensionMod:CarPart
    {
        public SportSuspensionMod(bool state) : base("SportSuspension", state)
        {
            Cost = 1230;
        }
    }
}
