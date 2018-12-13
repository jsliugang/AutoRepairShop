namespace AutoRepairShop.Data.Models.Humans
{
    internal class RmSanSanuch : RepairMan, ICanCustomize<RepairMan>, ICanDiagnoze<RepairMan>, ICanRepair<RepairMan>,
        ICanReplaceFluids<RepairMan>, ICanReplace<RepairMan>
    {
        public static readonly RmSanSanuch SanSanuch = new RmSanSanuch();
        int ICanReplace<RepairMan>.Priority { get; } = 2;
        int ICanDiagnoze<RepairMan>.Priority { get; } = 2;
        int ICanRepair<RepairMan>.Priority { get; } = 2;
        int ICanCustomize<RepairMan>.Priority { get; } = 2;
        int ICanReplaceFluids<RepairMan>.Priority { get; } = 2;

        private RmSanSanuch()
        {
            Name = "San-Sanuch";
        }
    }
}