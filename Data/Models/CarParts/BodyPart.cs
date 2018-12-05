namespace AutoRepairShop.Data.Models.CarParts
{
    class BodyPart:CarPart
    {
        public BodyPart(byte durability):base("Body", durability)
        {
            Cost = 1000;
        }
    }
}
