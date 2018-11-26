using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Data
{
    abstract class CarPartStock : IStock<CarPart>
    {
        public List<CarPart> Stock = new List<CarPart>();

        public CarPart ProvideItem()
        {
            if (Stock.Any())
            {
                CarPart part = Stock.Last();
                Stock.Remove(Stock.Last());
                StockCount();
                return part;
            }
            Console.WriteLine($"No item found");
            return null;
        }

        public void StockCount()
        {
            IStock <CarPart> pp = new BodyPartStock();
            foreach (CarPart part in Stock)
            {
                Console.WriteLine($"Part: {part}");
            }
        }
    }
}
