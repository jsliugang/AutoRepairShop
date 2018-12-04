using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmSanSanuch: RepairMan, ICanCustomize<RepairMan>, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>, ICanReplaceFluids<RepairMan>, ICanReplace<RepairMan>
    {
        public static readonly RmSanSanuch SanSanuch = new RmSanSanuch();

        private RmSanSanuch()
        {
            Name = "San-Sanuch";
            Priority = 2;
        }

        public int Modify(Car car, string modificationType)
        {
            Console.WriteLine($"Applying modifications to {car.Name}");
            return PerformModification(modificationType, car);
        }

        public int PerformModification(string partName, Car car)
        {
            CarPart newPart = CheckPartAvailability(partName);
            if (newPart != null)
            {
                car.CarContent.Add(newPart);
                Thread.Sleep(15000);
                Console.WriteLine($"All done!");
                return car.CarContent.Last().Cost;
            }
            Console.WriteLine($"{Name}: {partName} is not in garage, we have to request it from Stock.");
            if (RequestPartFromStock(partName))
            {
                return PerformModification(partName, car);
            }
            return 0;
        }

        public int ReplaceFluid(Car car, string liquid)
        {
            //if (liquid != null && car.CarLiquids.CarLiquids.ContainsKey(liquid))
            //{
            Console.WriteLine($"{Name}: Getting access to tank.");
            Thread.Sleep(15000);
            car.CarLiquids.CarLiquids[liquid] = 100;
            Console.WriteLine($"{Name}: All done!");
            //}
            return 50; //specify fluid costs
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
