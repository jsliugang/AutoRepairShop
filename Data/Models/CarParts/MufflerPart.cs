namespace AutoRepairShop.Data.Models.CarParts
{
    class MufflerPart:CarPart
    {
        public MufflerPart(bool state) : base("Muffler", state)
        {
            Cost = 100;
        }
    }
}
