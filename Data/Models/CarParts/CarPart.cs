using System;

namespace AutoRepairShop.Data.Models.CarParts
{
    abstract class CarPart
    {
        public string Name { get; }
        public bool IsWorking { get; set; }
        public int Cost { get; set; }
        public byte Durability { get; set; }

        public CarPart(){}

        public CarPart(string name, byte durability)
        {
            Name = name;
            Durability = durability;
            IsWorking = Durability > 0;
        }

        protected void BreakPart()
        {
            if (IsWorking)
            {
                IsWorking = !IsWorking;
            }
            else
            {
                Console.WriteLine($"{Name} is already broken!");
            }
        }
    }
}
