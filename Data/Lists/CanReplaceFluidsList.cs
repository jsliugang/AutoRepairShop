using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    [Synchronization]
    static class CanReplaceFluidsList
    {
        public static List<ICanReplaceFluids<RepairMan>> RepairMen = new List<ICanReplaceFluids<RepairMan>>();

        static CanReplaceFluidsList()
        {
            RepairMen.Add(RmPetrovich.Petrovich);
            RepairMen.Add(RmSanSanuch.SanSanuch);
            RepairMen.Add(RmVano.Vano);
            RepairMen.Add(RmLeha.Leha);
            RepairMen.Add(RmSerega.Serega);
        }
    }
}
