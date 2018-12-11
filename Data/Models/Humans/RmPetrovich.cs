using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmPetrovich:RepairMan, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>, ICanReplaceFluids<RepairMan>, ICanCustomize<RepairMan>
    {
        public static readonly RmPetrovich Petrovich = new RmPetrovich();
        int ICanDiagnoze<RepairMan>.Priority { get; } = 1;
        int ICanRepair<RepairMan>.Priority { get; } = 1;
        int ICanReplaceFluids<RepairMan>.Priority { get; } = 1;
        int ICanCustomize<RepairMan>.Priority { get; } = 3;

        private RmPetrovich()
        {
            Name = "Petrovich";
        }

        public override void Modify(string modificationType, Car car)
        {
            base.Modify(modificationType, car);
            var modification = car.CarContent.Find(x => x.Name == modificationType);
            modification.Durability -= 25;
        }
    }
}
