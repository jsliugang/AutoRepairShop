using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    static class CanReplaceFluidsList
    {
        public static List<ICanReplaceFluids<RepairMan>> RepairMen = new List<ICanReplaceFluids<RepairMan>>();

        static CanReplaceFluidsList()
        {
            RepairMen.Add(RmSanSanuch.SanSanuch);
            RepairMen.Add(RmPetrovich.Petrovich);
        }
    }
}
