using System;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal class RmSerega : RepairMan, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>,
        ICanReplaceFluids<RepairMan>, ICanReplace<RepairMan>
    {
        public static readonly RmSerega Serega = new RmSerega();
        int ICanDiagnoze<RepairMan>.Priority { get; } = 1;
        int ICanRepair<RepairMan>.Priority { get; } = 1;
        int ICanReplace<RepairMan>.Priority { get; } = 3;
        int ICanReplaceFluids<RepairMan>.Priority { get; } = 1;

        private RmSerega()
        {
            Name = "Serega";
        }

        public override void ReplacePart(string partName, Car car, ConsoleColor clr)
        {
            base.ReplacePart(partName, car, clr);
            var part = car.CarContent.Find(x => x.Name == partName);
            part.Durability -= 25;
        }
    }
}
