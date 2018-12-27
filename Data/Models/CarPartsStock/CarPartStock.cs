using System;
using System.Collections.Generic;
using System.Linq;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Repository;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    class CarPartStock<T> : IStock<CarPart>
    {
        public List<CarPart> Stock = new List<CarPart>();
        protected Random rand = new Random();

        public CarPart ProvideItem()
        {
            if (Stock.Any())
            {
                CarPart part = Stock.Last();
                Stock.Remove(Stock.Last());
                return part;
            }
            Console.WriteLine($"No item found");
            return null;
        }

        public virtual void AddMany(int amount)
        {           
        }

        public byte SetRandomDurability()
        {
            // TEST CODE
            //byte temp;
            //if (rand.NextDouble() > 0.2)
            //{
            //    temp = (byte)rand.Next(60, 101);
            //    Console.WriteLine($"Durability set to {temp}");
            //}
            //else
            //{
            //    temp = 0;
            //    Console.WriteLine($"Durability set to {temp}");
            //}
            //return temp;
            return rand.NextDouble() > 0.2 ? (byte)rand.Next(60, 101) : (byte)0;
        }
    }
}