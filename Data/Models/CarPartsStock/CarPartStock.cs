using System;
using System.Collections.Generic;
using System.Linq;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Repository;

namespace AutoRepairShop.Data.Models.CarPartsStock
{
    internal class CarPartStock<T> : IStock<CarPart>
    {
        public List<CarPart> Stock = new List<CarPart>();
        protected Random Rand = new Random();

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
            return Rand.NextDouble() > 0.2 ? (byte)Rand.Next(60, 101) : (byte)0;
        }
    }
}