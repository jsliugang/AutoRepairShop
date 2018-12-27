namespace AutoRepairShop.Data.Models.CarParts
{
    internal abstract class CarPart
    {
        public string Name { get; }
        public bool IsWorking { get; set; }
        public int Cost { get; set; }
        public byte Durability { get; set; }

        protected CarPart(string name, byte durability)
        {
            Name = name;
            Durability = durability;
            IsWorking = Durability > 0;
        }
    }
}
