using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal class RmKirill : RepairMan, ICanCustomize<RepairMan>, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>,
        ICanReplace<RepairMan>
    {
        public static readonly RmKirill Kirill = new RmKirill();
        int ICanCustomize<RepairMan>.Priority { get; } = 1;
        int ICanDiagnoze<RepairMan>.Priority { get; } = 1;
        int ICanRepair<RepairMan>.Priority { get; } = 1;
        int ICanReplace<RepairMan>.Priority { get; } = 3;

        private RmKirill()
        {
            Name = "Kirill";
            LastName = "Artemovich";
        }

        public override void ReplacePart(string partName, Car car)
        {
            base.ReplacePart(partName, car);
            var part = car.CarContent.Find(x => x.Name == partName);
            part.Durability -= 25;
        }
    }
}
