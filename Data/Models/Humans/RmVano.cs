using System;
using System.Threading;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmVano:RepairMan, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>, ICanReplace<RepairMan>
    {
        public static readonly RmVano Vano = new RmVano();

        private RmVano()
        {
            Name = "Vano";
            Priority = 1;
        }

        public int ReplacePart(string partName, Car car)
        {
            CarPart newPart = CheckPartAvailability(partName);
            CarPart oldPart = car.CarContent.Find(x => x.Name == partName);
            if (newPart != null)
            {
                Disassemble(oldPart);
                Thread.Sleep(5000);
                car.CarContent.Find(x => x.Name == partName).IsWorking = newPart.IsWorking;
                Console.WriteLine($"Replacing the broken part with new one!");
                Thread.Sleep(10000);
                Assemble(oldPart);
                return oldPart.Cost;
            }
            Console.WriteLine($"{Name}: {oldPart.Name} is not in garage, we have to request it from Stock.");
            if (RequestPartFromStock(oldPart.Name))
            {
                return ReplacePart(oldPart.Name, car);
            }
            return 0;
        }       
    }
}
