using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Data
{
    class CarPartStock<T> : IStock<CarPart>
        //where T : CarPart
    {
        public List<CarPart> Stock = new List<CarPart>();

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
    }
}
