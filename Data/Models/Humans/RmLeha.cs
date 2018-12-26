using System;
using System.Linq;
using System.Threading;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Tools;

namespace AutoRepairShop.Data.Models.Humans
{
    internal class RmLeha: RepairMan, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>, ICanCustomize<RepairMan>, ICanReplaceFluids<RepairMan>
    {
        public static readonly RmLeha Leha = new RmLeha();
        int ICanCustomize<RepairMan>.Priority { get; } = 1;
        int ICanDiagnoze<RepairMan>.Priority { get; } = 1;
        int ICanRepair<RepairMan>.Priority { get; } = 1;
        int ICanReplaceFluids<RepairMan>.Priority { get; } = 3;

        private RmLeha()
        {
            Name = "Leha";
        }

        public override int ReplaceFluid(Car car, ConsoleColor clr)
        {
            Say($"{Name}: Replacing fluids.", clr);
            for (var i = 0; i < car.CarLiquids.CarLiquids.Count; i++)
                car.CarLiquids.UpdateAmount(car.CarLiquids.CarLiquids.ElementAt(i).Key, 75);
            Thread.Sleep(TimeTool.ConvertToRealTime(3) * TimeTool.Thousand);
            Say($"{Name}: All done!", clr);
            return 150;
        }
    }
}
