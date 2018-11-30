using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmKirill:RepairMan, ICanCustomize<RepairMan>, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>
    {
        public static readonly RmKirill Kirill = new RmKirill();

        private RmKirill()
        {
            Name = "Kirill Artemovich";
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
    }
}
