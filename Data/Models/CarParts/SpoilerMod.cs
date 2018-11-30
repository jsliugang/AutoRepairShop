namespace AutoRepairShop.Data.Models.CarParts
{
    class SpoilerMod:CarPart
    {
        public SpoilerMod(bool state) : base("Spoiler", state)
        {
            Cost = 230;
        }
    }
}
