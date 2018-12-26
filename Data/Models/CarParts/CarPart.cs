namespace AutoRepairShop.Data.Models.CarParts
{
    internal abstract class CarPart
    {
        public string Name { get; }
        public int Cost { get; set; }
        public byte Durability { get; set; }
        public string Manufacturer { get; set; }

        protected CarPart(string name, byte durability)
        {
            Name = name;
            Durability = durability;
        }
    }
}
