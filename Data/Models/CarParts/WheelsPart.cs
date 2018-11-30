namespace AutoRepairShop.Data.Models.CarParts
{
    class WheelsPart:CarPart
    {
        public WheelsPart(bool state) : base("Wheels", state)
        {
            Cost = 500;
        }
    }
}
