namespace AutoRepairShop.Data.Models.CarParts
{
    class BodyPart:CarPart
    {
        public BodyPart(bool state):base("Body", state)
        {
            Cost = 1000;
        }
    }
}
