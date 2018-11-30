namespace AutoRepairShop.Data.Models.CarParts
{
    class DecalsMod:CarPart
    {
        public DecalsMod(bool state) : base("Decals", state)
        {
            Cost = 100;
        }
    }
}
