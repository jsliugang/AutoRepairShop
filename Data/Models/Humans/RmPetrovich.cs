using System;
using System.Threading;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmPetrovich:RepairMan, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>, ICanReplaceFluids<RepairMan>
    {
        public static readonly RmPetrovich Petrovich = new RmPetrovich();

        private RmPetrovich()
        {
            Name = "Petrovich";
        }

        public int ReplaceFluid(Car car, string liquid)
        {
                if (liquid != null && car.CarLiquids.CarLiquids.ContainsKey(liquid))
                {
                    Console.WriteLine($"{Name}: Getting access to tank.");
                    Thread.Sleep(15000);
                    car.CarLiquids.CarLiquids[liquid] = 100;
                    Console.WriteLine($"{Name}: All done!");
                }
        return 50; //specify fluid costs
        }
    }
}
