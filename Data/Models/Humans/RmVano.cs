using System;
using System.Threading;
using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmVano:RepairMan
    {
        public static readonly RmVano Vano = new RmVano();

        private RmVano()
        {
            Name = "Vano";
        }

        public int ReplacePart(CarPart part, Car car)
        {
            CarPart newPart = CheckPartAvailability(part.Name);
            if (newPart != null)
            {
                Disassemble(part);
                Thread.Sleep(5000);
                car.CarContent.Find(x => x.Name == part.Name).IsWorking = newPart.IsWorking;
                Console.WriteLine($"Replacing the broken part with new one!");
                Thread.Sleep(10000);
                Assemble(part);
                return part.Cost;
            }
            Console.WriteLine($"{Name}: {part.Name} is not in garage, we have to request it from Stock.");
            if (RequestPartFromStock(part.Name))
            {
                return ReplacePart(part, car);
            }
            return 0;
        }       
    }
}
