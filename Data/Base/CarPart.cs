using System;

namespace AutoRepairShop.Data.Base
{
    abstract class CarPart
    {
        public string Name { get; }
        public bool IsWorking { get; set; }
        public int Cost { get; set; }

        public CarPart(){}

        public CarPart(string name, bool state)
        {
            IsWorking = state;
            Name = name;
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
