using System;
using System.Linq;
using System.Threading;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal class RmVano : RepairMan, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>, ICanReplace<RepairMan>,
        ICanReplaceFluids<RepairMan>
    {
        public static readonly RmVano Vano = new RmVano();

        int ICanReplace<RepairMan>.Priority { get; } = 1;
        int ICanDiagnoze<RepairMan>.Priority { get; } = 1;
        int ICanRepair<RepairMan>.Priority { get; } = 1;
        int ICanReplaceFluids<RepairMan>.Priority { get; } = 3;

        private RmVano()
        {
            Name = "Vano";
        }

        public override int ReplaceFluid(Car car)
        {
            Console.WriteLine($"{Name}: Replacing fluids.");
            for (var i = 0; i < car.CarLiquids.CarLiquids.Count; i++)
                car.CarLiquids.UpdateAmount(car.CarLiquids.CarLiquids.ElementAt(i).Key, 75);
            Thread.Sleep(15000);
            Console.WriteLine($"{Name}: All done!");
            return 150;
        }
    }
}
