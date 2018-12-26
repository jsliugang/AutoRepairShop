using System;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal class RmMihaluch : RepairMan, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>, ICanCustomize<RepairMan>, ICanReplace<RepairMan>
    {
        public static readonly RmMihaluch Mihaluch = new RmMihaluch();
        int ICanCustomize<RepairMan>.Priority { get; } = 3;
        int ICanDiagnoze<RepairMan>.Priority { get; } = 1;
        int ICanRepair<RepairMan>.Priority { get; } = 1;
        int ICanReplace<RepairMan>.Priority { get; } = 1;

        private RmMihaluch()
        {
            Name = "Mihaluch";
        }

        public override void Modify(string modificationType, Car car, ConsoleColor clr)
        {
            base.Modify(modificationType, car, clr);
            var modification = car.CarContent.Find(x => x.Name == modificationType);
            modification.Durability -= 25;
        }
    }
}
