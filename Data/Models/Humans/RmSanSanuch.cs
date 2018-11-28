using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmSanSanuch: RepairMan
    {
        public static readonly RmSanSanuch SanSanuch = new RmSanSanuch();

        private RmSanSanuch()
        {
            Name = "San-Sanuch";
        }
    }
}
